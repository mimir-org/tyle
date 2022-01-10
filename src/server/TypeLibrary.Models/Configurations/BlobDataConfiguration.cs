using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Configurations
{
    public class BlobDataConfiguration : IEntityTypeConfiguration<BlobData>
    {
        public void Configure(EntityTypeBuilder<BlobData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("BlobData");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Data).HasColumnName("Data").IsRequired();
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>();
        }
    }
}
