# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY DigitalWallet.Domain/*.csproj ./DigitalWallet.Domain/
COPY DigitalWallet.Aplication/*.csproj ./DigitalWallet.Aplication/
COPY DigitalWallet.Infrastructure/*.csproj ./DigitalWallet.Infrastructure/
COPY wallet-api/*.csproj ./wallet-api/

RUN dotnet restore

COPY . .
WORKDIR /app/wallet-api
RUN dotnet publish -c Release -o /out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "wallet-api.dll"]
