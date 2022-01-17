using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class NodeTypeTerminalTypeConfiguration : IEntityTypeConfiguration<NodeTypeTerminalType>
    {
        public void Configure(EntityTypeBuilder<NodeTypeTerminalType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("NodeType_TerminalType");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Number).HasColumnName("Number").IsRequired().HasDefaultValue(1);
            builder.Property(p => p.ConnectorType).HasColumnName("ConnectorType").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.TerminalType).WithMany(y => y.NodeTypes).HasForeignKey(x => x.TerminalTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.NodeType).WithMany(y => y.TerminalTypes).HasForeignKey(x => x.NodeTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
