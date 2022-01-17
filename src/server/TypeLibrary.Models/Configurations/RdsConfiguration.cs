using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class RdsConfiguration : IEntityTypeConfiguration<Rds>
    {
        public void Configure(EntityTypeBuilder<Rds> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Rds");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Code).HasColumnName("Code").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.SemanticReference).HasColumnName("SemanticReference").IsRequired(false);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.RdsCategory).WithMany(y => y.RdsList).HasForeignKey(x => x.RdsCategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
