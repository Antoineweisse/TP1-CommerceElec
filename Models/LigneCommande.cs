using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class LigneCommande
{
    public int Id { get; set; }

    public int CommandeId { get; set; }

    [ForeignKey("CommandeId")]
    public virtual Commande Commande { get; set; } = null!;

    public int ProduitId { get; set; }

    [ForeignKey("ProduitId")]
    public virtual Produit Produit { get; set; } = null!;

    public int Quantite { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrixUnitaire { get; set; }
}
