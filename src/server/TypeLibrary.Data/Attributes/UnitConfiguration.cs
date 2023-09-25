using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class UnitConfiguration : IEntityTypeConfiguration<RdlUnit>
{
    public void Configure(EntityTypeBuilder<RdlUnit> builder)
    {
        builder.ToTable("Unit");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>();
        builder.Property(x => x.Source).IsRequired().HasConversion<string>();
    }
}