using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class Panier
{
    public int Id { get; set; }
    public int UtilisateurId { get; set; }

    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    // Un panier contient des LIGNES de panier (Items)
    public virtual ICollection<PanierItem> Items { get; set; } = new List<PanierItem>();
}