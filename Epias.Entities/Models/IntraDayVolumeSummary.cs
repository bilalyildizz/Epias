using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;
public partial class IntraDayVolumeSummary : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public int Period { get; set; }
    public double Volume { get; set; }
    public string? PeriodType { get; set; }
}

