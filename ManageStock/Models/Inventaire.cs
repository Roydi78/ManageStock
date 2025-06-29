using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Inventaire
{
    public int IdInventaire { get; set; }

    public int? IdEntrepot { get; set; }

    public DateOnly? DateInventaire { get; set; }

    public string? Commentaire { get; set; }

    public virtual ICollection<Detailinventaire> Detailinventaires { get; set; } = new List<Detailinventaire>();

    public virtual Entrepot? IdEntrepotNavigation { get; set; }
}
