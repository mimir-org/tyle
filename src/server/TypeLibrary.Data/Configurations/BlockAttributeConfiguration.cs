using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class BlockAttributeConfiguration : IEntityTypeConfiguration<BlockAttributeLibDm>
{
    public void Configure(EntityTypeBuilder<BlockAttributeLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Block_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p=>p.PartOfAttributeGroup).HasColumnName("PartOfAttributeGroup").HasMaxLength(63);

        builder.HasOne(x => x.Block).WithMany(y => y.BlockAttributes).HasForeignKey(x => x.BlockId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeBlocks).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.Cascade);
        
    }
}