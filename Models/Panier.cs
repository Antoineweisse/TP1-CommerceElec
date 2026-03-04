using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class Panier
{
    public int Id { get; set; }

    public int UtilisateurId { get; set; }

    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();


}