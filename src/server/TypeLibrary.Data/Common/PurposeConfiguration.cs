using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Data.Common;

public class PurposeConfiguration : IEntityTypeConfiguration<RdlPurpose>
{
    public void Configure(EntityTypeBuilder<RdlPurpose> builder)
    {
        builder.ToTable("Purpose");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>();
        builder.Property(x => x.Source).IsRequired().HasConversion<string>();
    }
}