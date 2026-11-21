# Projekt DevOps – Aplikacja Webowa (.NET 10)

Projekt zaliczeniowy prezentujący pełny proces **CI/CD** (Continuous Integration / Continuous Deployment) dla aplikacji webowej w technologii **.NET 10 Minimal API**, hostowanej w chmurze **Microsoft Azure App Service**.

---

## Funkcjonalności i Endpointy

Aplikacja udostępnia dwa główne endpointy REST API:

| Metoda | Endpoint | Opis |
|--------|----------|------|
| `GET` | `/` | Zwraca status działania aplikacji (`OK`), komunikat powitalny oraz aktualnie wdrożoną wersję (`Version`). |
| `GET` | `/products` | Zwraca listę produktów w formacie JSON wraz z logowaniem zapytań. |

---

## Zrealizowane elementy projektu

### Część obowiązkowa:
1. **Taski i backlog projektu** – Tablica Kanban w *GitHub Projects* z podziałem na zadania (*Issues*) powiązane z commitami oraz Pull Requestami.
2. **Repozytorium i Git** – Praca na gałęziach funkcjonalnych (*Feature Branches*) mergowanych przez *Pull Request* do gałęzi `main`.
3. **Aplikacja + testy** – Aplikacja webowa w .NET 10 (`/` oraz `/products`) oraz zestaw 3 testów jednostkowych/integracyjnych w **xUnit**.
4. **Pipeline CI** – Automatyczne budowanie, weryfikacja jakości kodu i uruchamianie testów przy każdym Pull Requeście wraz z generowaniem raportu z testów w *GitHub Actions*.
5. **Pipeline CD** – Automatyczne wdrażanie aplikacji po zmergowaniu do `main` do usługi **Azure App Service**.

### Część dodatkowa (Do wyboru):
- **7. Konteneryzacja** – Plik `Dockerfile` wykorzystujący wieloetapowe budowanie (*multi-stage build*) dla .NET 10.
- **8. Monitoring i logi** – Logowanie zdarzeń w endpointach za pomocą `ILogger` zintegrowane z **Azure Application Insights / Log Stream**.
- **9. Automatyczne wersjonowanie aplikacji** – Dynamiczne nadawanie wersji semantycznej `1.0.<run_number>` podczas budowania w pipeline CI/CD.
- **11. Sekrety i bezpieczne przechowywanie danych** – Przechowywanie danych uwierzytelniających do chmury (`AZURE_WEBAPP_NAME`, `AZURE_WEBAPP_PUBLISH_PROFILE`) w zaszyfrowanym magazynie **GitHub Secrets**.
- **13. Quality Gate w CI/CD** – Wymuszenie kompilacji bez ostrzeżeń (`-warnaserror`), pozytywnego przejścia 100% testów oraz *Branch Protection Rules* blokujących merge przed przejściem CI.

---

## Instrukcja uruchomienia lokalnego

### Wymagania
- Zainstalowane środowisko **.NET 10 SDK**
- *(Opcjonalnie)* **Docker Desktop**

### 1. Uruchomienie aplikacji z konsoli / IDE (Rider)
```bash
dotnet restore "Project dev ops.sln"
dotnet run --project "Project dev ops/Project dev ops.csproj"
```
Po uruchomieniu aplikacja jest dostępna pod adresem wyświetlanym w terminalu (domyślnie `http://localhost:5000/` oraz `http://localhost:5000/products`).

### 2. Uruchomienie testów jednostkowych
```bash
dotnet test "Project dev ops.sln"
```

### 3. Uruchomienie w kontenerze Docker
```bash
docker build -t project-devops:latest .
docker run -p 8080:8080 project-devops:latest
```
Po uruchomieniu kontenera aplikacja odpowiada pod adresem `http://localhost:8080/` oraz `http://localhost:8080/products`.
