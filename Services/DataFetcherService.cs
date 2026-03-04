using System.Net.Http.Json;
using tp1.Models;

public class DataFetcherService
{
    private readonly HttpClient _httpClient;

    public DataFetcherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Produit>> GetProductsFromApiAsync()
    {
        
        var response = await _httpClient.GetFromJsonAsync<List<FakeProductDto>>("https://fakestoreapi.com/products");
        return response?.Select(p => new Produit
        {
            Titre = p.title,
            Prix = p.price,
            Description = p.description,
            Categorie = p.category,
            ImageUrl = p.image
        }).ToList() ?? new List<Produit>();
    }
}
public record FakeProductDto(string title, decimal price, string description, string category, string image);