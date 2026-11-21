using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProjectDevOps.Tests;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        // Tworzy testowego klienta HTTP, który uruchamia naszą aplikację w pamięci (bez otwierania prawdziwego portu)
        _client = factory.CreateClient();
    }

    // TEST 1: Testuje endpoint "/" (czy aplikacja żyje i zwraca status 200 OK)
    [Fact]
    public async Task RootEndpoint_ReturnsSuccessAndCorrectMessage()
    {
        // Act (Działanie)
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        // Assert (Sprawdzenie wyniku)
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Aplikacja DevOps działa poprawnie!", content);
    }

    // TEST 2: Testuje endpoint "/products" (czy zwraca listę produktów jako JSON ze statusem 200 OK)
    [Fact]
    public async Task ProductsEndpoint_ReturnsListOfProducts()
    {
        // Act (Działanie)
        var response = await _client.GetAsync("/products");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        // Assert (Sprawdzenie wyniku)
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(products);
        Assert.Equal(4, products.Count);
    }

    // TEST 3: Czysty test jednostkowy logiki katalogu produktów (sprawdza poprawność cen i nazw)
    [Fact]
    public void ProductCatalog_GetProducts_ContainsValidData()
    {
        // Act (Działanie)
        var products = ProductCatalog.GetProducts();

        // Assert (Sprawdzenie wyniku)
        Assert.NotEmpty(products);
        Assert.All(products, product =>
        {
            Assert.False(string.IsNullOrWhiteSpace(product.Name));
            Assert.True(product.Price > 0);
        });
    }

    // TEST 4: Test wyszukiwania produktu po ID (dodany w ramach Pull Requesta dla Zadania #2)
    [Fact]
    public void ProductCatalog_GetById_ReturnsMonitorForId4()
    {
        // Act (Działanie)
        var product = ProductCatalog.GetById(4);

        // Assert (Sprawdzenie wyniku)
        Assert.NotNull(product);
        Assert.Equal("Monitor LG UltraFine 4K", product.Name);
    }
}
