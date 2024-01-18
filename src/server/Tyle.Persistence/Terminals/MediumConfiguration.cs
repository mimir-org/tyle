using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tyle.Application.Common;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class MediumConfiguration : IEntityTypeConfiguration<RdlMedium>
{
    public void Configure(EntityTypeBuilder<RdlMedium> builder)
    {
        builder.ToTable("Medium");

        builder.HasIndex(b => b.Iri).IsUnique();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Source).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
    }
}