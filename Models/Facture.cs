using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class Facture
{
    public int Id { get; set; }

    public int UtilisateurId { get; set; }

    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    
    public int VendeurId { get; set; }

    [ForeignKey("VendeurId")]
    public virtual Utilisateur Vendeur { get; set; } = null!;
}