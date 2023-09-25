using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Data.Terminals;

public class MediumConfiguration : IEntityTypeConfiguration<RdlMedium>
{
    public void Configure(EntityTypeBuilder<RdlMedium> builder)
    {
        builder.ToTable("Medium");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>();
        builder.Property(x => x.Source).IsRequired().HasConversion<string>();
    }
}