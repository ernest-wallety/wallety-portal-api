using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Wallety.Portal.Api.dto;
using Wallety.Portal.Api.Exceptions;
using Wallety.Portal.Application.Configuration;
using Wallety.Portal.Application.Handlers.Users;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Infrastructure.Repository;
using Wallety.Portal.Infrastructure.Services;

namespace Wallety.Portal.Api
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        public IConfiguration Configuration = configuration;
        private readonly IWebHostEnvironment _env = env;


        public void ConfigureServices(IServiceCollection services)
        {
            // Add compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY") ?? Configuration["Values:SECRET_KEY"]!))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Place your JWT Bearer token in the Text-Box below.",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins(
                    "https://wallety-portal-dev.svc-eu2.zcloud.ws",
                    "https://wallety-portal.svc-eu2.zcloud.ws",
                    "https://app.wallety.cash",
                    "http://localhost:3000",
                    "http://localhost:4200",
                    "http://localhost:4201"
                ).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            }));
            services.AddMemoryCache();

            // JSON serialization (optimized for all environments)
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.WriteIndented = _env.IsDevelopment(); // Pretty print only in dev
                });

            services.AddApiVersioning();
            services.AddHealthChecks();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallety Portal API", Version = "v1" }); });

            //DI
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();

            services.AddSingleton(typeof(ILogger), logger);
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            // JM: Register the global exception handler
            services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();

            // Add Azure Repository Service
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersHandler).Assembly));

            //Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IOutboundMailRepository, OutboundMailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();

            // Component Interfaces
            services.AddScoped<IPgSqlSelector>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                var conString = Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                    ?? configuration.GetConnectionString("PGSQL_CONNECTION_STRING");

                return new PgSqlSelector(conString!);
            });

            //service cache
            services.AddScoped<ICachingInMemoryService, CachingInMemoryService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.AddSingleton<IConfigurationService, ConfigurationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            // This setup allows for centralized handling of exceptions in an ASP.NET Core application,
            // ensuring that all unhandled exceptions are captured and processed according to the application's error handling logic.
            app.UseExceptionHandler((Action<IApplicationBuilder>)(errorApp =>
            {
                errorApp.Run((RequestDelegate)(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature?.Error;

                    if (exception != null)
                    {
                        var handler = context.RequestServices.GetRequiredService<IExceptionHandler>();
                        await handler.TryHandleAsync(context, exception, context.RequestAborted);
                    }
                }));
            }));

            app.UseHsts();

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseCors("ApiCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
