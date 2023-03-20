using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<UnitLibDm>
    {
        public void Configure(EntityTypeBuilder<UnitLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.State).IsUnique(false);
            builder.ToTable("Unit");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.TypeReferences).HasColumnName("TypeReferences");
            builder.Property(p => p.Symbol).HasColumnName("Symbol").HasMaxLength(31);
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        }
    }
}