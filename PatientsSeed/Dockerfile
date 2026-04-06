FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PatientsSeed/PatientsSeed.csproj", "PatientsSeed/"]
RUN dotnet restore "PatientsSeed/PatientsSeed.csproj"

COPY . .
WORKDIR "/src/PatientsSeed"
RUN dotnet publish "PatientsSeed.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PatientsSeed.dll"]