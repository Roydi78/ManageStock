using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Stock
{
    public int IdStock { get; set; }

    public int? IdProduit { get; set; }

    public int? IdEntrepot { get; set; }

    [Display(Name = "Qté Dispo.")]
    public int? QuantitéDisponible { get; set; }

    [Display(Name = "Entrepot")]
    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    [Display(Name = "Produit")]
    public virtual Produit? IdProduitNavigation { get; set; }
}
