using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDayTradeHistory : IEntity
{
    public long Id { get; set; }
    public long IdApi { get; set; }
    public DateTime Date { get; set; }
    public string Contract { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

