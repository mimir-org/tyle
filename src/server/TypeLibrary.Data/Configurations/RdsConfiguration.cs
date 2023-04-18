using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class RdsConfiguration : IEntityTypeConfiguration<RdsLibDm>
{
    public void Configure(EntityTypeBuilder<RdsLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => new { x.RdsCode, x.Name });
        builder.HasIndex(x => x.State).IsUnique(false);
        builder.ToTable("Rds");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
        builder.Property(p => p.RdsCode).HasColumnName("RdsCode").IsRequired().HasMaxLength(15);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
        builder.Property(p => p.TypeReference).HasColumnName("TypeReference").HasMaxLength(255);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
        builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);

        builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
    }
}