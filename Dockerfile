# ETAP 1: Budowanie aplikacji (używamy pełnego obrazu SDK .NET 10.0)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Kopiujemy plik projektu i pobieramy zależności (NuGet)
COPY ["Project dev ops/Project dev ops.csproj", "Project dev ops/"]
RUN dotnet restore "Project dev ops/Project dev ops.csproj"

# Kopiujemy resztę kodu i publikujemy aplikację w trybie produkcyjnym (Release)
COPY . .
WORKDIR "/src/Project dev ops"
RUN dotnet publish "Project dev ops.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAP 2: Uruchamianie aplikacji (używamy lekkiego obrazu uruchomieniowego ASP.NET 10.0)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

# Komenda startująca aplikację wewnątrz kontenera
ENTRYPOINT ["dotnet", "Project dev ops.dll"]
