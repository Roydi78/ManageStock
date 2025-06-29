using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Categorie
{
    public int IdCategorie { get; set; }

    [Required(ErrorMessage = "La catégorie doit nécessairement avoir un nom")]
    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
