using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Configurations
{
    public class MimirorgCompanyConfiguration : IEntityTypeConfiguration<MimirorgCompany>
    {
        public void Configure(EntityTypeBuilder<MimirorgCompany> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable("MimirorgCompany");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.DisplayName).HasColumnName("DisplayName").IsRequired(false);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Secret).HasColumnName("Secret").IsRequired(false);
            builder.Property(p => p.Domain).HasColumnName("Domain").IsRequired(true);
            builder.Property(p => p.Iris).HasColumnName("Iris").IsRequired(false);
            builder.HasOne(x => x.Manager).WithMany(y => y.MangeCompanies).HasForeignKey(x => x.ManagerId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
