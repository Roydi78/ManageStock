using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    [Required(ErrorMessage = "Nom obligatoire")]
    public string Nom { get; set; } = null!;

    public string? Adresse { get; set; }

    [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Téléphone obligatoire")]
    [RegularExpression(@"^\+?[0-9\s\-]{7,15}$", ErrorMessage = "Numéro de téléphone invalide.")]
    public string? Téléphone { get; set; }

    public virtual ICollection<Entreestock> Entreestocks { get; set; } = new List<Entreestock>();

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
