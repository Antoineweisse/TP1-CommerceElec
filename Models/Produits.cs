using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1.Models;

public class Produit
{
    public int Id { get; set; }

    [Required]
    public required string Titre { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Prix { get; set; }

    [Required]
    public required string Categorie { get; set; }

    [Required]
    public required string ImageUrl { get; set; }

    public int VendeurId { get; set; }
    [ForeignKey("VendeurId")]
    public Utilisateur? Vendeur { get; set; }
}