using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class TerminalConfiguration : IEntityTypeConfiguration<TerminalLibDm>
{
    public void Configure(EntityTypeBuilder<TerminalLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.State).IsUnique(false);
        builder.HasIndex(x => x.FirstVersionId).IsUnique(false);
        builder.HasIndex(x => new { x.ParentId }).IsUnique(false);
        builder.ToTable("Terminal");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(31);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
        builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
        builder.Property(p => p.TypeReference).HasColumnName("TypeReference").HasMaxLength(255);
        builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(7);
        builder.Property(p => p.FirstVersionId).HasColumnName("FirstVersionId").IsRequired().HasMaxLength(31);
        builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
        builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.Color).HasColumnName("Color").IsRequired().HasMaxLength(7);
        builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
        builder.Property(p => p.ParentId).HasColumnName("ParentId").IsRequired(false).HasMaxLength(31);

        builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
    }
}