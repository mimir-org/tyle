using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("RefreshToken");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.ClientId).HasColumnName("ClientId").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired();
            builder.Property(p => p.Secret).HasColumnName("Secret").IsRequired();
            builder.Property(p => p.ValidTo).HasColumnName("ValidTo").IsRequired();
        }
    }
}
