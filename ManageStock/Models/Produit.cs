using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Produit
{
    public int IdProduit { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public string? CodeBarres { get; set; }

    public decimal? PrixUnitaire { get; set; }

    public int? SeuilAlerte { get; set; }

    public int? IdCategorie { get; set; }

    public int? IdFournisseur { get; set; }

    public virtual ICollection<Detailinventaire> Detailinventaires { get; set; } = new List<Detailinventaire>();

    public virtual ICollection<Entreestock> Entreestocks { get; set; } = new List<Entreestock>();

    public virtual Categorie? IdCategorieNavigation { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }

    public virtual ICollection<Sortiestock> Sortiestocks { get; set; } = new List<Sortiestock>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
