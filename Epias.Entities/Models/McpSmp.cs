using System;
using System.Collections.Generic;
using Epias.Entities.Interfaces;

namespace Epias.Models
{
    public partial class McpSmp:IEntity
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Mcp { get; set; }
        public decimal Smp { get; set; }
        public string SmpDirection { get; set; } = null!;
        public string McpState { get; set; } = null!;
    }
}
