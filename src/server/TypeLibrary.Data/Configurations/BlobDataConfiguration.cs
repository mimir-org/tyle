using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class BlobDataConfiguration : IEntityTypeConfiguration<BlobLibDm>
    {
        public void Configure(EntityTypeBuilder<BlobLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Blob");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Data).HasColumnName("Data").IsRequired();
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>();
        }
    }
}
