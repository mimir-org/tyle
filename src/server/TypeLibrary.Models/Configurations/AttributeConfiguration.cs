using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Configurations.Converters;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<AttributeLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeLibDm> builder)
        {
            var stringComparer = new StringHashSetValueComparer();
            var stringConverter = new StringHashSetValueConverter();

            builder.HasKey(x => x.Id);
            builder.ToTable("Attribute");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Entity).HasColumnName("Entity").IsRequired();
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>();
            builder.Property(p => p.SelectValuesString).HasColumnName("SelectValuesString").IsRequired(false);
            builder.Property(p => p.SelectType).HasColumnName("Select").IsRequired().HasConversion<string>();
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>();
            builder.Property(p => p.Tags).HasColumnName("Tags").IsRequired(false).HasConversion(stringConverter, stringComparer);

            builder.HasOne(x => x.ConditionDm).WithMany(y => y.AttributeList).HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.QualifierDm).WithMany(y => y.AttributeList).HasForeignKey(x => x.QualifierId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.SourceDm).WithMany(y => y.AttributeList).HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FormatDm).WithMany(y => y.AttributeList).HasForeignKey(x => x.FormatId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FormatDm).WithMany(y => y.AttributeList).HasForeignKey(x => x.FormatId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Units).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("Attribute_Unit",
                x => x.HasOne<UnitLibDm>().WithMany().HasForeignKey("UnitId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Unit")
            );

            builder.HasMany(x => x.NodeTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("Node_Attribute",
                x => x.HasOne<NodeLibDm>().WithMany().HasForeignKey("NodeId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Node_Attribute")
            );

            builder.HasMany(x => x.TransportTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("Transport_Attribute",
                x => x.HasOne<TransportLibDm>().WithMany().HasForeignKey("TransportId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Transport_Attribute")
            );

            builder.HasMany(x => x.SimpleTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("Simple_Attribute",
                x => x.HasOne<SimpleLibDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Simple_Attribute")
            );
        }
    }
}
