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
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Code).HasColumnName("Code").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);

            builder.HasOne(x => x.RdsCategory).WithMany(y => y.RdsList).HasForeignKey(x => x.RdsCategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
