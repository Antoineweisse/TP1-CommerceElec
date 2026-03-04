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

    public int IDStripe { get; set; }

    public DateTime Date { get; set; }

    public double Total { get; set; }

    public StatutCommande Statut { get; set; }

    public int UtilisateurId { get; set; }

    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;
   
}