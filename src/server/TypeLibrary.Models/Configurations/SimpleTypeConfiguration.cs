using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class SimpleTypeConfiguration : IEntityTypeConfiguration<SimpleType>
    {
        public void Configure(EntityTypeBuilder<SimpleType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("SimpleType");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);

            builder.HasMany(x => x.AttributeTypes).WithMany(y => y.SimpleTypes).UsingEntity<Dictionary<string, object>>("SimpleType_AttributeType",
                x => x.HasOne<AttributeType>().WithMany().HasForeignKey("AttributeTypeId"),
                x => x.HasOne<SimpleType>().WithMany().HasForeignKey("SimpleTypeId"),
                x => x.ToTable("SimpleType_AttributeType")
            );

            builder.HasMany(x => x.NodeTypes).WithMany(y => y.SimpleTypes).UsingEntity<Dictionary<string, object>>("SimpleType_NodeType",
                x => x.HasOne<NodeType>().WithMany().HasForeignKey("NodeTypeId"),
                x => x.HasOne<SimpleType>().WithMany().HasForeignKey("SimpleTypeId"),
                x => x.ToTable("SimpleType_NodeType")
            );
        }
    }
}