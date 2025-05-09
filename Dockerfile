# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY wallet-api.sln ./
COPY src/DigitalWallet.Domain/*.csproj ./src/DigitalWallet.Domain/
COPY src/DigitalWallet.Aplication/*.csproj ./src/DigitalWallet.Aplication/
COPY src/DigitalWallet.Infrastructure/*.csproj ./src/DigitalWallet.Infrastructure/
COPY src/wallet-api/*.csproj ./src/wallet-api/

RUN dotnet restore

COPY . ./
WORKDIR /app/src/wallet-api
RUN dotnet publish -c Release -o /out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "wallet-api.dll"]
