# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Enable compression and optimize build
ENV ASPNETCORE_RESPONSECOMPRESSION_ENABLED=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=true
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

# Expose the web and ssl port
EXPOSE 80 
EXPOSE 443
EXPOSE 8080
EXPOSE 5136

# Copy only project files first for better layer caching
COPY ["./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj", "./"]
COPY ["./src/Wallety.Portal.Application/Wallety.Portal.Application.csproj", "./"]
COPY ["./src/Wallety.Portal.Core/Wallety.Portal.Core.csproj", "./"]
COPY ["./src/Wallety.Portal.Infrastructure/Wallety.Portal.Infrastructure.csproj", "./"]

RUN dotnet restore "./Wallety.Portal.Api.csproj"

# Copy remaining files and build
COPY . .
RUN dotnet build "./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish with trimming and ready-to-run
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish 
   #  /p:UseAppHost=false \
   #  /p:PublishTrimmed=true \
   #  /p:EnableCompressionInSingleFile=true

# Final stage with minimal runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Configure compression and performance
ENV ASPNETCORE_RESPONSECOMPRESSION_ENABLED=true
ENV DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_HTTP2SUPPORT=true
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Wallety.Portal.Api.dll"]
