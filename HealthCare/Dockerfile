# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HealthCare/HealthCare.csproj", "HealthCare/"]
RUN dotnet restore "HealthCare/HealthCare.csproj"

COPY . .
WORKDIR "/src/HealthCare"
RUN dotnet build "HealthCare.csproj" -c Release -o /app/build

# publish
FROM build AS publish
RUN dotnet publish "HealthCare.csproj" -c Release -o /app/publish /p:UseAppHost=false

# final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "HealthCare.dll"]