using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class RdsConfiguration : IEntityTypeConfiguration<RdsLibDm>
    {
        public void Configure(EntityTypeBuilder<RdsLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Rds");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired();
            builder.Property(p => p.TypeReferences).HasColumnName("TypeReferences");
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
        }
    }
}