using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Data.Terminals;

public class MediumConfiguration : IEntityTypeConfiguration<RdlMedium>
{
    public void Configure(EntityTypeBuilder<RdlMedium> builder)
    {
        builder.ToTable("Medium");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Source).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
    }
}