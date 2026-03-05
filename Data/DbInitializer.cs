using tp1.Models;
using tp1.Data;
using System.Security.Cryptography;
using System.Text;

public static class DbInitializer
{
    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
    public static async Task SeedData(ApplicationDbContext context, DataFetcherService fetcher)
    {
        context.Database.EnsureCreated();

        if (context.Produits.Any())
        {
            return;
        }

        var produitsApi = await fetcher.GetProductsFromApiAsync();

        
        var vendeurParDefaut = new Utilisateur 
        { 
            Nom = "Vendeur", 
            Email = "vendeur@uqar.ca", 
            Role = TypeUtilisateur.Vendeur,
            MotDePasse = DbInitializer.HashPassword("password"),
            Prenom = "Admin"
            
        };
        context.Utilisateurs.Add(vendeurParDefaut);
        await context.SaveChangesAsync();

        foreach (var p in produitsApi)
        {
            p.VendeurId = vendeurParDefaut.Id;
            context.Produits.Add(p);
        }

        await context.SaveChangesAsync();
    }
}