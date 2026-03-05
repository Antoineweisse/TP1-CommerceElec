using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class Facture
{
    public int Id { get; set; }

    public DateTime DateFacture { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal MontantTotal { get; set; }

    public string? PaiementId { get; set; }

    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    public virtual Utilisateur Client { get; set; } = null!;

    public int CommandeId { get; set; }

    [ForeignKey("CommandeId")]
    public virtual Commande Commande { get; set; } = null!;
}