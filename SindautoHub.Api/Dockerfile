# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia apenas os csproj necessários (sem os testes)
COPY SindautoHub.Api/*.csproj SindautoHub.Api/
COPY SindautoHub.Application/*.csproj SindautoHub.Application/
COPY SindautoHub.Infrastructure/*.csproj SindautoHub.Infrastructure/
COPY SindautoHub.Domain/*.csproj SindautoHub.Domain/

# Restaura dependências baseado no projeto principal (API)
RUN dotnet restore SindautoHub.Api/SindautoHub.Api.csproj

# Copia o restante do código
COPY . .

# Publica a aplicação
RUN dotnet publish SindautoHub.Api/SindautoHub.Api.csproj -c Release -o /publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "SindautoHub.Api.dll"]
