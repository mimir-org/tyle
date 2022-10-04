using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Configurations
{
    public class MimirorgUserConfiguration : IEntityTypeConfiguration<MimirorgUser>
    {
        public void Configure(EntityTypeBuilder<MimirorgUser> builder)
        {
            builder.HasIndex(x => new { x.FirstName, x.LastName }).IsUnique(false);
            builder.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
            builder.Property(p => p.LastName).HasColumnName("LastName").IsRequired();
            builder.Property(p => p.SecurityHash).HasColumnName("SecurityHash").IsRequired();
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
            builder.Property(p => p.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Purpose).HasColumnName("Purpose").IsRequired().HasMaxLength(255);
        }
    }
}