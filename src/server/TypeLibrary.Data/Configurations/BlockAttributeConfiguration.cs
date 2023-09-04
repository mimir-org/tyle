using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class BlockAttributeConfiguration : IEntityTypeConfiguration<BlockAttributeTypeReference>
{
    public void Configure(EntityTypeBuilder<BlockAttributeTypeReference> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Block_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(p => p.MinCount).HasColumnName("MinCount").IsRequired();
        builder.Property(p => p.MaxCount).HasColumnName("MaxCount");
    }
}