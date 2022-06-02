using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;

public partial class IntraDaySummary : IEntity
{
    public long Id { get; set; }
    public long IdApi { get; set; }
    public string Contract { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal MaxAskPrice { get; set; }
    public decimal MaxBidPrice { get; set; }
    public decimal MaxMatchPrice { get; set; }
    public decimal MinAskPrice { get; set; }
    public decimal MinBidPrice { get; set; }
    public decimal MinMatchPrice { get; set; }
    public double QuantityOfAsk { get; set; }
    public double QuantityOfBid { get; set; }
    public double TradingVolume { get; set; }
    public double Volume { get; set; }
}

