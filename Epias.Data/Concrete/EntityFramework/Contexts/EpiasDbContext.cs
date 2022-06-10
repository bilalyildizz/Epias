using System;
using System.Collections.Generic;
using Epias.Entities.Models;
using Epias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Epias.Data.Concrete.EntityFramework.Contexts;

public partial class EpiasDbContext : DbContext
{
    public EpiasDbContext()
    {
    }

    public EpiasDbContext(DbContextOptions<EpiasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IntraDayAof> IntraDayAof { get; set; }
    public virtual DbSet<IntraDayAofAverage> IntraDayAofAverage { get; set; }
    public virtual DbSet<IntraDayIncome> IntraDayIncome { get; set; }
    public virtual DbSet<IntraDayIncomeSummary> IntraDayIncomeSummary { get; set; }
    public virtual DbSet<IntraDayQuantity> IntraDayQuantity { get; set; }
    public virtual DbSet<IntraDaySummary> IntraDaySummary { get; set; }
    public virtual DbSet<IntraDayTradeHistory> IntraDayTradeHistory { get; set; }
    public virtual DbSet<IntraDayVolume> IntraDayVolume { get; set; }
    public virtual DbSet<IntraDayVolumeSummary> IntraDayVolumeSummary { get; set; }
    public virtual DbSet<McpSmp> McpSmp { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

