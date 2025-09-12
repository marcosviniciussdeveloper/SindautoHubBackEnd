# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os csproj necessários
COPY SindautoHub.Api/*.csproj SindautoHub.Api/
COPY SindautoHub.Application/*.csproj SindautoHub.Application/
COPY SindautoHub.Infrastructure/*.csproj SindautoHub.Infrastructure/
COPY SindautoHub.Domain/*.csproj SindautoHub.Domain/

# Restaura dependências
RUN dotnet restore SindautoHub.Api/SindautoHub.Api.csproj

# Copia todo o código
COPY . .

# Publica a aplicação
WORKDIR /src/SindautoHub.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Fly.io usa a porta 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SindautoHub.Api.dll"]
