using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDayQuantity : IEntity
{
    public long Id { get; set; }
    public decimal HourlyPurchaseQuantity { get; set; }
    public DateTime EffectiveDate { get; set; }
    public decimal HourlySaleQuantity { get; set; }
}

