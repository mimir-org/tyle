using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.Common.Converters;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<AttributeLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeLibDm> builder)
        {
            var stringComparer = new StringHashSetValueComparer();
            var stringConverter = new StringHashSetValueConverter();

            builder.HasKey(x => x.Id);
            builder.ToTable("Attribute");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.ContentReferences).HasColumnName("ContentReferences");
            builder.Property(p => p.Deleted).HasColumnName("Deleted").IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.SelectValuesString).HasColumnName("SelectValuesString").IsRequired(false);
            builder.Property(p => p.Select).HasColumnName("Select").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>().HasMaxLength(63);
            builder.Property(p => p.Tags).HasColumnName("Tags").IsRequired(false).HasConversion(stringConverter, stringComparer);
            builder.Property(p => p.AttributeQualifier).HasColumnName("AttributeQualifier").HasMaxLength(31);
            builder.Property(p => p.AttributeSource).HasColumnName("AttributeSource").HasMaxLength(31);
            builder.Property(p => p.AttributeCondition).HasColumnName("AttributeCondition").HasMaxLength(31);
            builder.Property(p => p.AttributeFormat).HasColumnName("AttributeFormat").HasMaxLength(31);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null).HasMaxLength(31);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Units).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Unit",
                x => x.HasOne<UnitLibDm>().WithMany().HasForeignKey("UnitId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Unit")
            );

            builder.HasMany(x => x.Nodes).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Node",
                x => x.HasOne<NodeLibDm>().WithMany().HasForeignKey("NodeId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Node")
            );

            builder.HasMany(x => x.Transports).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Transport",
                x => x.HasOne<TransportLibDm>().WithMany().HasForeignKey("TransportId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Transport")
            );

            builder.HasMany(x => x.Simple).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Simple",
                x => x.HasOne<SimpleLibDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Simple")
            );

            builder.HasMany(x => x.Interfaces).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Interface",
                x => x.HasOne<InterfaceLibDm>().WithMany().HasForeignKey("InterfaceId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Interface")
            );
        }
    }
}
