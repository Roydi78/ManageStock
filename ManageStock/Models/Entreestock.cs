using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Entreestock
{
    public int IdEntree { get; set; }

    [Display(Name = "Produit")]
    public int? IdProduit { get; set; }

    [Display(Name = "Qté.")]
    public int Quantité { get; set; }

    [Display(Name = "Date d'entrée")]
    public DateTime? DateEntree { get; set; }

    [Display(Name = "Fournisseur")]
    public int? IdFournisseur { get; set; }

    [Display(Name = "Entrepot")]
    public int? IdEntrepot { get; set; }

    [Display(Name = "Entrepot")]
    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    [Display(Name = "Fournisseur")]
    public virtual Fournisseur? IdFournisseurNavigation { get; set; }

    [Display(Name = "Produit")]
    public virtual Produit? IdProduitNavigation { get; set; }
}
