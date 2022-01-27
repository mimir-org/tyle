using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class SimpleTypeConfiguration : IEntityTypeConfiguration<SimpleDm>
    {
        public void Configure(EntityTypeBuilder<SimpleDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Simple");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);

            builder.HasMany(x => x.AttributeList).WithMany(y => y.SimpleTypes).UsingEntity<Dictionary<string, object>>("Simple_Attribute",
                x => x.HasOne<AttributeDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.HasOne<SimpleDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.ToTable("Simple_Attribute")
            );

            builder.HasMany(x => x.NodeTypes).WithMany(y => y.SimpleTypes).UsingEntity<Dictionary<string, object>>("Simple_Node",
                x => x.HasOne<NodeDm>().WithMany().HasForeignKey("NodeId"),
                x => x.HasOne<SimpleDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.ToTable("Simple_Node")
            );
        }
    }
}