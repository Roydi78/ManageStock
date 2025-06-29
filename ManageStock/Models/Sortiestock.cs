using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Sortiestock
{
    public int IdSortie { get; set; }

    public int? IdProduit { get; set; }

    public int Quantité { get; set; }

    public DateTime? DateSortie { get; set; }

    public string? Destination { get; set; }

    public int? IdEntrepot { get; set; }

    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    public virtual Produit? IdProduitNavigation { get; set; }
}
