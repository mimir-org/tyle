using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Configurations
{
    public class MimirorgHookConfiguration : IEntityTypeConfiguration<MimirorgHook>
    {
        public void Configure(EntityTypeBuilder<MimirorgHook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("MimirorgHook");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
            builder.Property(p => p.Key).HasColumnName("Key").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired();
            
            builder.HasOne(x => x.Company).WithMany(y => y.Hooks).HasForeignKey(x => x.CompanyId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
