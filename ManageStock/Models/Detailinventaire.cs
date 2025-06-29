using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Detailinventaire
{
    public int IdDetail { get; set; }

    public int? IdInventaire { get; set; }

    public int? IdProduit { get; set; }

    public int QuantitéComptée { get; set; }

    public virtual Inventaire? IdInventaireNavigation { get; set; }

    public virtual Produit? IdProduitNavigation { get; set; }
}
