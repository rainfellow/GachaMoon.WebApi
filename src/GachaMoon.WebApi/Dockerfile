
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG TARGETARCH
WORKDIR "/build"

COPY ["Directory.Build.props", "."]
COPY ["src/GachaMoon.WebApi/GachaMoon.WebApi.csproj", "GachaMoon.WebApi/"]

RUN dotnet restore "GachaMoon.WebApi/GachaMoon.WebApi.csproj" -a $TARGETARCH

COPY ["nuget.config", "."]
COPY [".", "."]

WORKDIR "/build/src/GachaMoon.WebApi"

RUN dotnet publish -c Release -o /app/publish -a $TARGETARCH --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GachaMoon.WebApi.dll"]