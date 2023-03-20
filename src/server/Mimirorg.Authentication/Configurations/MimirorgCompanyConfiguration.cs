using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Configurations;

public class MimirorgCompanyConfiguration : IEntityTypeConfiguration<MimirorgCompany>
{
    public void Configure(EntityTypeBuilder<MimirorgCompany> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Domain).IsUnique();
        builder.HasIndex(x => new { x.Domain, x.Secret }).IsUnique();
        builder.ToTable("MimirorgCompany");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.DisplayName).HasColumnName("DisplayName").IsRequired(false);
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
        builder.Property(p => p.Secret).HasColumnName("Secret").IsRequired();
        builder.Property(p => p.Domain).HasColumnName("Domain").IsRequired();
        builder.Property(p => p.Logo).HasColumnName("Logo").IsRequired(false);
        builder.Property(p => p.HomePage).HasColumnName("HomePage").IsRequired(false).HasMaxLength(255);
        builder.HasOne(x => x.Manager).WithMany(y => y.MangeCompanies).HasForeignKey(x => x.ManagerId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}