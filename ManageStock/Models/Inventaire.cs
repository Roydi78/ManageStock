using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageStock.Models;

public partial class Inventaire
{
    public int IdInventaire { get; set; }

    [Display(Name = "Entrepot")]
    public int? IdEntrepot { get; set; }

    [Display(Name = "Date Inventaire")]
    public DateOnly? DateInventaire { get; set; }

    public string? Commentaire { get; set; }

    public virtual ICollection<Detailinventaire> Detailinventaires { get; set; } = new List<Detailinventaire>();

    [Display(Name = "Entrepot")]
    public virtual Entrepot? IdEntrepotNavigation { get; set; }
}
