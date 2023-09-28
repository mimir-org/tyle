using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Data.Attributes;

public class UnitConfiguration : IEntityTypeConfiguration<RdlUnit>
{
    public void Configure(EntityTypeBuilder<RdlUnit> builder)
    {
        builder.ToTable("Unit");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Source).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
        builder.Property(x => x.Symbol).HasMaxLength(StringLengthConstants.UnitSymbolLength);
    }
}