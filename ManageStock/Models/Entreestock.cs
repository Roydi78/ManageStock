using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Entreestock
{
    public int IdEntree { get; set; }

    public int? IdProduit { get; set; }

    public int Quantité { get; set; }

    public DateTime? DateEntree { get; set; }

    public int? IdFournisseur { get; set; }

    public int? IdEntrepot { get; set; }

    public virtual Entrepot? IdEntrepotNavigation { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }

    public virtual Produit? IdProduitNavigation { get; set; }
}
