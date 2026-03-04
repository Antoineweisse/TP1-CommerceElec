using System.ComponentModel.DataAnnotations;
namespace tp1.Models;

public enum TypeUtilisateur
{
    Client,
    Vendeur
}

public class Utilisateur
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string MotDePasse { get; set; }

        [Required]
        public TypeUtilisateur Role { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        public required string Prenom { get; set; }
    }