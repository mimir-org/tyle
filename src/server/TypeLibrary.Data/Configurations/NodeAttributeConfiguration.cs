using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations;

public class NodeAttributeConfiguration : IEntityTypeConfiguration<NodeAttributeLibDm>
{
    public void Configure(EntityTypeBuilder<NodeAttributeLibDm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Node_Attribute");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(x => x.Node).WithMany(y => y.NodeAttributes).HasForeignKey(x => x.NodeId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Attribute).WithMany(y => y.AttributeNodes).HasForeignKey(x => x.AttributeId).OnDelete(DeleteBehavior.NoAction);
    }
}