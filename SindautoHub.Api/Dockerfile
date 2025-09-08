# Etapa 1 - build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos e restaura dependências
COPY ["../SindautoHub.sln", "./"]
COPY ["../SindautoHub.Api/SindautoHub.Api.csproj", "SindautoHub.Api/"]
COPY ["../SindautoHub.Application/SindautoHub.Application.csproj", "SindautoHub.Application/"]
COPY ["../SindautoHub.Domain/SindautoHub.Domain.csproj", "SindautoHub.Domain/"]
COPY ["../SindautoHub.Infrastructure/SindautoHub.Infrastructure.csproj", "SindautoHub.Infrastructure/"]
RUN dotnet restore "./SindautoHub.Api/SindautoHub.Api.csproj"

# Copia todo o restante e faz o build
COPY .. .
WORKDIR "/app/SindautoHub.Api"
RUN dotnet publish "SindautoHub.Api.csproj" -c Release -o /publish

# Etapa 2 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "SindautoHub.Api.dll"]
otnet", "SindautoHub.Api.dll"]