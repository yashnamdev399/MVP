# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src
COPY CarRecommendation.Api/*.csproj ./CarRecommendation.Api/
RUN dotnet restore CarRecommendation.Api/CarRecommendation.Api.csproj
COPY CarRecommendation.Api/ ./CarRecommendation.Api/
RUN dotnet publish CarRecommendation.Api/CarRecommendation.Api.csproj \
    -c Release \
    -o /app \
    --no-restore \
    /p:DebugType=none \
    /p:DebugSymbols=false

# Stage 2: Runtime (alpine for minimal image size)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=build /app .

# Railway sets PORT at runtime — shell form expands it for Kestrel
ENTRYPOINT sh -c "dotnet CarRecommendation.Api.dll --urls http://0.0.0.0:${PORT:-5000}"
