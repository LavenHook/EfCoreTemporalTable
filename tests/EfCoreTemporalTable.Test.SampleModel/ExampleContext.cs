using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreTemporalTable.Test.SampleModel
{
  public class ExampleContext : DbContext
  {
    public DbSet<ExampleEntity1> ExampleEntity1 { get; private set; }

    public ExampleContext(DbContextOptions<ExampleContext> options)
        : base(options)
    { 
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ExampleEntity1Configuration());
      base.OnModelCreating(modelBuilder);
    }
  }
}
