
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app


COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /out --no-restore /p:DebugType=none /p:DebugSymbols=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT sh -c "dotnet CarRecommendation.Api.dll --urls http://0.0.0.0:${PORT:-5000}"