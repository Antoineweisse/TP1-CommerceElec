using tp1.Models;
using tp1.Data;

public static class DbInitializer
{
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
            Nom = "Admin Vendeur", 
            Email = "vendeur@uqar.ca", 
            Role = TypeUtilisateur.Vendeur,
            MotDePasse = "password",
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