using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Configurations.Converters;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class CollaborationPartnerConfiguration : IEntityTypeConfiguration<CollaborationPartner>
    {
        public void Configure(EntityTypeBuilder<CollaborationPartner> builder)
        {
            var stringComparer = new StringCollectionValueComparer();
            var stringConverter = new StringCollectionValueConverter();

            builder.HasKey(x => x.Id);
            builder.ToTable("CollaborationPartner");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Domain).HasColumnName("Domain").IsRequired();
            builder.Property(p => p.Current).HasColumnName("Current").IsRequired();
            builder.Property(p => p.Iris).HasColumnName("Iris").IsRequired(false).HasConversion(stringConverter, stringComparer);

            builder.HasIndex(x => x.Domain).IsUnique();
        }
    }
}
