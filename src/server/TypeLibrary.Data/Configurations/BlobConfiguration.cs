using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class BlobConfiguration : IEntityTypeConfiguration<BlobLibDm>
    {
        public void Configure(EntityTypeBuilder<BlobLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Blob");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Iri).HasColumnName("Iri").HasMaxLength(255);
            builder.Property(p => p.Data).HasColumnName("Data").IsRequired();
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>().HasMaxLength(63);
        }
    }
}
