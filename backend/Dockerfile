# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# 1. Hamein nested folder ka specific path copy karna hoga
COPY CarRecommendation.Api/*.csproj ./CarRecommendation.Api/
RUN dotnet restore CarRecommendation.Api/*.csproj

# 2. Saara baki backend code copy karo
COPY . .

# 3. Project file ka path dekar publish chalao
RUN dotnet publish CarRecommendation.Api/*.csproj -c Release -o /app --no-restore /p:DebugType=none /p:DebugSymbols=false

# Stage 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=build /app .

# Dynamic port binding
ENTRYPOINT sh -c "dotnet CarRecommendation.Api.dll --urls http://0.0.0.0:${PORT:-5000}"