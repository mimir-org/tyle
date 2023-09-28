using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Data.Common;

public class PurposeConfiguration : IEntityTypeConfiguration<RdlPurpose>
{
    public void Configure(EntityTypeBuilder<RdlPurpose> builder)
    {
        builder.ToTable("Purpose");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(StringLengthConstants.NameLength);
        builder.Property(x => x.Description).HasMaxLength(StringLengthConstants.DescriptionLength);
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.IriLength);
        builder.Property(x => x.Source).IsRequired().HasConversion<string>().HasMaxLength(StringLengthConstants.EnumLength);
    }
}