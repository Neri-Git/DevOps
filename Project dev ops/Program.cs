using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint 1: Strona główna "/"
app.MapGet("/", (ILogger<Program> logger) =>
{
    var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
    logger.LogInformation("Wywołano endpoint główny (/). Wersja aplikacji: {Version}", version);

    return Results.Ok(new
    {
        Message = "Aplikacja DevOps działa poprawnie!",
        Status = "OK",
        Version = version
    });
});

// Endpoint 2: Lista produktów "/products"
app.MapGet("/products", (ILogger<Program> logger) =>
{
    var products = ProductCatalog.GetProducts();
    logger.LogInformation("Wywołano endpoint /products. Zwrócono {Count} produktów.", products.Count);

    return Results.Ok(products);
});

app.Run();

// Model produktu oraz katalog danych (wydzielone, aby łatwo pisać testy jednostkowe)
public record Product(int Id, string Name, decimal Price, string Category);

public static class ProductCatalog
{
    public static IReadOnlyList<Product> GetProducts() =>
    [
        new Product(1, "Laptop Dell XPS", 5499.99m, "Elektronika"),
        new Product(2, "Myszka Logitech MX Master", 399.00m, "Akcesoria"),
        new Product(3, "Klawiatura mechaniczna Keychron", 499.00m, "Akcesoria")
    ];
}

// Potrzebne do testów integracyjnych WebApplicationFactory
public partial class Program;
