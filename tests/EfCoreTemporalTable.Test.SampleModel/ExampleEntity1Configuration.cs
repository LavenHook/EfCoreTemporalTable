using EntityFrameworkCore.TemporalTables.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreTemporalTable.Test.SampleModel
{
  public class ExampleEntity1Configuration : IEntityTypeConfiguration<ExampleEntity1>
  {
    public void Configure(EntityTypeBuilder<ExampleEntity1> builder)
    {
      builder.HasKey(e => e.ExampleEntityId);
      builder.Property(e => e.FirstProperty)
        .HasColumnName("FirstMappedProperty");
      builder.Property(e => e.SecondProperty)
        .HasColumnName("SecondMappedProperty");
      builder.Property(e => e.ThirdProperty)
        .HasColumnName("ThirdMappedProperty");
      builder.Property(e => e.FourthProperty)
        .HasColumnName("FourthMappedProperty")
        .ValueGeneratedOnAddOrUpdate()
        .HasDefaultValueSql("NEWID()");

      builder.Property<DateTime>("SysStartTime").ValueGeneratedNever();
      builder.Ignore("SysStartTime");
      builder.Property<DateTime>("SysEndTime").ValueGeneratedNever();
      builder.Ignore("SysEndTime");
      builder.UseTemporalTable();
    }
  }
}
