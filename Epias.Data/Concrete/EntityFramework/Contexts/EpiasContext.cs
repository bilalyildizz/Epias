using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Epias.Data.Concrete.EntityFramework.Contexts;

public class EpiasContext : DbContext
{
    public EpiasContext(DbContextOptions<EpiasContext> options) : base(options)
    {

    }
    public DbSet<TradeHistory> TradeHistories { get; set; }
}

