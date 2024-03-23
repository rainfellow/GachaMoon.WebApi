FROM mcr.microsoft.com/dotnet/sdk:7.0.202-bullseye-slim as build-env
WORKDIR "/build"

COPY ["Directory.Build.props", "."]
COPY ["src/GachaMoon.WebApi/GachaMoon.WebApi.csproj", "GachaMoon.WebApi/"]

RUN dotnet restore "GachaMoon.WebApi/GachaMoon.WebApi.csproj"

COPY [".", "."]

WORKDIR "/build/src/GachaMoon.WebApi"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0.4-bullseye-slim as runtime
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GachaMoon.WebApi.dll"]
