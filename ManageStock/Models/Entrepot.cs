using System;
using System.Collections.Generic;

namespace ManageStock.Models;

public partial class Entrepot
{
    public int IdEntrepot { get; set; }

    public string Nom { get; set; } = null!;

    public string? Adresse { get; set; }

    public virtual ICollection<Entreestock> Entreestocks { get; set; } = new List<Entreestock>();

    public virtual ICollection<Inventaire> Inventaires { get; set; } = new List<Inventaire>();

    public virtual ICollection<Sortiestock> Sortiestocks { get; set; } = new List<Sortiestock>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
