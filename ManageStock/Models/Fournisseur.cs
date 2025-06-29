using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    public string Nom { get; set; } = null!;

    public string? Adresse { get; set; }

    public string? Email { get; set; }

    [Required(ErrorMessage = "Téléphone obligatoire")]
    public string? Téléphone { get; set; }

    public virtual ICollection<Entreestock> Entreestocks { get; set; } = new List<Entreestock>();

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
