# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio y sus proyectos referenciados
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar solo los archivos de proyecto primero para aprovechar la cache de Docker
COPY ["OmniStock.Api/OmniStock.Api.csproj", "OmniStock.Api/"]
COPY ["OmniStock.Aplicacion/OmniStock.Aplicacion.csproj", "OmniStock.Aplicacion/"]
COPY ["OmniStock.Infraestructura/OmniStock.Infraestructura.csproj", "OmniStock.Infraestructura/"]
COPY ["OmniStock.Dominio/OmniStock.Dominio.csproj", "OmniStock.Dominio/"]

RUN dotnet restore "OmniStock.Api/OmniStock.Api.csproj"

# Copiar el resto del código y construir
COPY . .
WORKDIR "/src/OmniStock.Api"
RUN dotnet build "OmniStock.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/OmniStock.Api"
RUN dotnet publish "OmniStock.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmniStock.Api.dll"]
