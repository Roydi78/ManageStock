using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Sortiestock
{
    public int IdSortie { get; set; }

    [Display(Name = "Produit")]
    public int? IdProduit { get; set; }

    [Display(Name = "Qté.")]
    public int Quantité { get; set; }

    [Display(Name = "Date de Sortie")]
    public DateTime? DateSortie { get; set; }

    public string? Destination { get; set; }

    [Display(Name = "Entrepot")]
    public int? IdEntrepot { get; set; }

    [Display(Name = "Entrepot")]
    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    [Display(Name = "Produit")]
    public virtual Produit? IdProduitNavigation { get; set; }
}
