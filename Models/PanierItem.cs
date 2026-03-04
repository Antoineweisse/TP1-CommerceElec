using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class PanierItem
{
    public int Id { get; set; }
    
    public int ProduitId { get; set; }
    [ForeignKey("ProduitId")]
    public virtual Produit Produit { get; set; } = null!;

    public int PanierId { get; set; }
    [ForeignKey("PanierId")]
    public virtual Panier Panier { get; set; } = null!;

    public int Quantite { get; set; }
}