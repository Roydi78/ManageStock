using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Produit
{
    public int IdProduit { get; set; }

    [Display(Name = "Produit")]
    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    [Display(Name = "Code Barres")]
    public string? CodeBarres { get; set; }

    [Display(Name = "P.U")]
    [Range(typeof(decimal), "0.01", "9999999999", ErrorMessage = "Le montant doit être supérieur à 0")]
    public decimal? PrixUnitaire { get; set; }

    [Display(Name = "Seuil Alerte")]
    public int? SeuilAlerte { get; set; }
    
    [Display(Name = "Catégorie")]
    public int? IdCategorie { get; set; }
    
    [Display(Name = "Fournisseur")]
    public int? IdFournisseur { get; set; }

    public virtual ICollection<Detailinventaire> Detailinventaires { get; set; } = new List<Detailinventaire>();

    public virtual ICollection<Entreestock> Entreestocks { get; set; } = new List<Entreestock>();

    [Display(Name = "Catégorie")]
    public virtual Categorie? IdCategorieNavigation { get; set; }

    [Display(Name = "Fournisseur")]
    public virtual Fournisseur? IdFournisseurNavigation { get; set; }

    public virtual ICollection<Sortiestock> Sortiestocks { get; set; } = new List<Sortiestock>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
