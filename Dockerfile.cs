# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Expose the web and ssl port
EXPOSE 80 
EXPOSE 443
EXPOSE 8080
EXPOSE 5136

COPY ["./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj", "./"]
COPY ["./src/Wallety.Portal.Application/Wallety.Portal.Application.csproj", "./"]
COPY ["./src/Wallety.Portal.Core/Wallety.Portal.Core.csproj", "./"]
COPY ["./src/Wallety.Portal.Infrastructure/Wallety.Portal.Infrastructure.csproj", "./"]

RUN dotnet restore "./Wallety.Portal.Api.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/Wallety.Portal.Api/Wallety.Portal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Wallety.Portal.Api.dll"]
