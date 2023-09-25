using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class PredicateConfiguration : IEntityTypeConfiguration<RdlPredicate>
{
    public void Configure(EntityTypeBuilder<RdlPredicate> builder)
    {
        builder.ToTable("Predicate");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>();
        builder.Property(x => x.Source).IsRequired().HasConversion<string>();
    }
}