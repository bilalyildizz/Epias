using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDayAof : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
}

