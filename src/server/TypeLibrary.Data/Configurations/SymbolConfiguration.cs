using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class SymbolConfiguration : IEntityTypeConfiguration<SymbolLibDm>
{
    public void Configure(EntityTypeBuilder<SymbolLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Symbol");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
        builder.Property(p => p.Iri).HasColumnName("Iri").HasMaxLength(255);
        builder.Property(p => p.TypeReferences).HasColumnName("TypeReferences");
        builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Data).HasColumnName("Data").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
    }
}