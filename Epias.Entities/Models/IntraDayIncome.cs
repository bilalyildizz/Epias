using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDayIncome : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
}

