using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class AttributeConfiguration : IEntityTypeConfiguration<AttributeType>
{
    public void Configure(EntityTypeBuilder<AttributeType> builder)
    {
        var stringConverter = new StringCollectionValueConverter();
        var stringComparer = new StringCollectionValueComparer();

        builder.HasKey(x => x.Id);
        //builder.HasIndex(x => x.State).IsUnique(false);
        builder.ToTable("Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(500);
        builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(10);
        builder.Property(p => p.CreatedOn).HasColumnName("CreatedOn").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(50);
        builder.Property(p => p.ContributedBy).HasColumnName("ContributedBy").IsRequired().HasConversion(stringConverter, stringComparer).HasMaxLength(2000);
        builder.Property(p => p.LastUpdateOn).HasColumnName("LastUpdateOn").IsRequired();
        //builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
        builder.Property(p => p.UnitMinCount).HasColumnName("UnitMinCount").IsRequired();
        builder.Property(p => p.UnitMaxCount).HasColumnName("UnitMaxCount").IsRequired();
        builder.Property(p => p.ProvenanceQualifier).HasColumnName("ProvenanceQualifier").HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.RangeQualifier).HasColumnName("RangeQualifier").HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.RegularityQualifier).HasColumnName("RegularityQualifier").HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.ScopeQualifier).HasColumnName("ScopeQualifier").HasConversion<string>().HasMaxLength(50);
    }
}