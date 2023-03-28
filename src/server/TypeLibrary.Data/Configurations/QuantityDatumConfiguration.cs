using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class QuantityDatumConfiguration : IEntityTypeConfiguration<QuantityDatumLibDm>
{
    public void Configure(EntityTypeBuilder<QuantityDatumLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.State).IsUnique(false);
        builder.ToTable("QuantityDatum");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
        builder.Property(p => p.TypeReference).HasColumnName("TypeReference").HasMaxLength(255);
        builder.Property(p => p.CompanyId).HasColumnName("CompanyId");
        builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
        builder.Property(p => p.QuantityDatumType).HasColumnName("QuantityDatumType").IsRequired()
            .HasConversion<string>().HasMaxLength(63);
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
    }
}