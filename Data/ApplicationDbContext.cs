using Microsoft.EntityFrameworkCore;
using tp1.Models;

namespace tp1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
        public DbSet<Produit> Produits { get; set; } = null!;

        public DbSet<Commande> Commandes { get; set; } = null!;

        public DbSet<Panier> Paniers { get; set; } = null!;

        public DbSet<PanierItem> PanierItems { get; set; } = null!;

        public DbSet<Facture> Factures { get; set; } = null!;

        public DbSet<LigneCommande> LigneCommandes { get; set; } = null!;
    }

}
