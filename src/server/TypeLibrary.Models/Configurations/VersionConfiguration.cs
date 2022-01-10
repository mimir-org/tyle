using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class VersionConfiguration : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Version");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Ver).HasColumnName("Ver").IsRequired();
            builder.Property(p => p.Type).HasColumnName("Type").IsRequired();
            builder.Property(p => p.TypeId).HasColumnName("TypeId").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired();
            builder.Property(p => p.Data).HasColumnName("Data").IsRequired();
        }
    }
}