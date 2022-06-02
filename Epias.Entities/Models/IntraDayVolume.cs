using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDayVolume : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double BlockMatchQuantity { get; set; }
    public double HourlyMatchQuantity { get; set; }
}

