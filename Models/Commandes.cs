using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;


public enum StatutCommande
{
    EnAttente,
    EnCours,
    Terminee,
    Annulee
}

public class Commande
{
    public int Id { get; set; }

    public string? PaiementId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public StatutCommande Statut { get; set; }

    public int UtilisateurId { get; set; }

    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    public virtual ICollection<LigneCommande> Lignes { get; set; } = new List<LigneCommande>();

    public int? FactureId { get; set; }
    public virtual Facture? Facture { get; set; }
}