using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epias.Entities.Concrete;

public class TradeHistory
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string Conract { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

