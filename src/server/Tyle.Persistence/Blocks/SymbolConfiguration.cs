using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Application.Common;
using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class SymbolConfiguration : IEntityTypeConfiguration<Symbol>
{
    public void Configure(EntityTypeBuilder<Symbol> builder)
    {
        builder.ToTable("Symbol");

        builder.Property(x => x.Label).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.SvgString).IsRequired();

        builder
            .HasMany(e => e.ConnectionPoints)
            .WithOne(e => e.Symbol)
            .HasForeignKey(e => e.SymbolId)
            .IsRequired();
    }
}
