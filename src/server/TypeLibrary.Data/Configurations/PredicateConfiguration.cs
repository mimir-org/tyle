using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class PredicateConfiguration : IEntityTypeConfiguration<PredicateReference>
{
    public void Configure(EntityTypeBuilder<PredicateReference> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Iri).IsUnique();
        builder.ToTable("Predicate");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(500);
        builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired().HasMaxLength(500);
        builder.Property(p => p.Source).HasColumnName("Source").HasConversion<string>().HasMaxLength(50);
    }
}