using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;
public partial class IntraDayIncomeSummary : IEntity
{
    public long Id { get; set; }
    public int Period { get; set; }
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public string PeriodType { get; set; } = null!;
}

