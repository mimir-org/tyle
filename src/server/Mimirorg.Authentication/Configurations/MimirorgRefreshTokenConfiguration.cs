using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Configurations
{
    public class MimirorgRefreshTokenConfiguration : IEntityTypeConfiguration<MimirorgRefreshToken>
    {
        public void Configure(EntityTypeBuilder<MimirorgRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ClientId).IsUnique(false);
            builder.HasIndex(x => x.Secret).IsUnique();
            builder.ToTable("MimirorgRefreshToken");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.ClientId).HasColumnName("ClientId").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired();
            builder.Property(p => p.Secret).HasColumnName("Secret").IsRequired();
            builder.Property(p => p.ValidTo).HasColumnName("ValidTo").IsRequired();
        }
    }
}
