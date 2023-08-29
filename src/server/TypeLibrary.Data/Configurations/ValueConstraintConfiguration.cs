using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Converters;

namespace TypeLibrary.Data.Configurations;

public class ValueConstraintConfiguration : IEntityTypeConfiguration<ValueConstraintLibDm>
{
    public void Configure(EntityTypeBuilder<ValueConstraintLibDm> builder)
    {
        var stringConverter = new StringCollectionValueConverter();
        var stringComparer = new StringCollectionValueComparer();

        builder.HasKey(x => x.Id);
        builder.ToTable("Value_Constraint");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.ConstraintType).HasColumnName("ConstraintType").IsRequired().HasConversion<string>().HasMaxLength(20);
        builder.Property(p => p.Value).HasColumnName("Value").HasMaxLength(500);
        builder.Property(p => p.AllowedValues).HasColumnName("AllowedValues").HasConversion(stringConverter, stringComparer).HasMaxLength(500);
        builder.Property(p => p.DataType).HasColumnName("DataType").IsRequired().HasConversion<string>().HasMaxLength(20);
        builder.Property(p => p.MinCount).HasColumnName("MinCount");
        builder.Property(p => p.MaxCount).HasColumnName("MaxCount");
        builder.Property(p => p.Pattern).HasColumnName("Pattern").HasMaxLength(500);
        builder.Property(p => p.MinValue).HasColumnName("MinValue").HasPrecision(38, 19);
        builder.Property(p => p.MaxValue).HasColumnName("MaxValue").HasPrecision(38, 19);
        builder.Property(p => p.MinInclusive).HasColumnName("MinInclusive");
        builder.Property(p => p.MaxInclusive).HasColumnName("MaxInclusive");
    }
}