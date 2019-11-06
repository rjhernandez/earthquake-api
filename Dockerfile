FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY api/*.csproj ./api/
COPY generate_token/*.csproj ./generate_token/
RUN dotnet restore

# copy everything else and build app
COPY api/. ./api/
WORKDIR /app/api
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/api/out ./
ENTRYPOINT ["dotnet", "EarthquakeApi.dll"]