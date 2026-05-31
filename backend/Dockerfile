# Stage 1: Application ko build karna (.NET 10 SDK use karke)
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

# 1. Sabse pehle .csproj file ko backend root se copy karo
COPY *.csproj ./
RUN dotnet restore

# 2. Baki saari C# files ko copy karke publish/compile karo
COPY . ./
RUN dotnet publish -c Release -o /out --no-restore /p:DebugType=none /p:DebugSymbols=false

# Stage 2: Sirf runtime container banana (taaki size chota rahe)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=build /out .

# Railway ya Render automatically PORT set karte hain, ye line use handle karegi
ENTRYPOINT sh -c "dotnet CarRecommendation.Api.dll --urls http://0.0.0.0:${PORT:-5000}"