using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Stock
{
    public int IdStock { get; set; }

    public int? IdProduit { get; set; }

    public int? IdEntrepot { get; set; }

    public int? QuantitéDisponible { get; set; }

    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    public virtual Produit? IdProduitNavigation { get; set; }
}
