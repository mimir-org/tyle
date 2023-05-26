using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Converters;

namespace TypeLibrary.Data.Configurations;

public class AspectObjectConfiguration : IEntityTypeConfiguration<AspectObjectLibDm>
{
    public void Configure(EntityTypeBuilder<AspectObjectLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.FirstVersionId).IsUnique(false);
        builder.HasIndex(x => x.State).IsUnique(false);
        builder.HasIndex(x => new { x.State, x.Aspect }).IsUnique(false);
        builder.ToTable("AspectObject");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(63);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
        builder.Property(p => p.TypeReference).HasColumnName("TypeReference").HasMaxLength(255);
        builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(7);
        builder.Property(p => p.FirstVersionId).HasColumnName("FirstVersionId").IsRequired().HasMaxLength(63);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
        builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.PurposeName).HasColumnName("PurposeName").IsRequired().HasMaxLength(127);
        builder.Property(p => p.Symbol).HasColumnName("Symbol").HasMaxLength(127);
        builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
        builder.Property(p => p.SelectedAttributePredefined).HasJsonConversion();

        builder.HasOne(x => x.Rds).WithMany().HasForeignKey(x => x.RdsId).OnDelete(DeleteBehavior.NoAction).IsRequired();
    }
}