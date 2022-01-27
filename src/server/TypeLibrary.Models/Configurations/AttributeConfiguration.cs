using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Configurations.Converters;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            var stringComparer = new StringHashSetValueComparer();
            var stringConverter = new StringHashSetValueConverter();

            builder.HasKey(x => x.Id);
            builder.ToTable("Attribute");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Entity).HasColumnName("Entity").IsRequired();
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>();
            builder.Property(p => p.SelectValuesString).HasColumnName("SelectValuesString").IsRequired(false);
            builder.Property(p => p.SelectType).HasColumnName("SelectType").IsRequired().HasConversion<string>();
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>();
            builder.Property(p => p.Tags).HasColumnName("Tags").IsRequired(false).HasConversion(stringConverter, stringComparer);

            builder.HasOne(x => x.Condition).WithMany(y => y.AttributeList).HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Qualifier).WithMany(y => y.AttributeList).HasForeignKey(x => x.QualifierId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Source).WithMany(y => y.AttributeList).HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Format).WithMany(y => y.AttributeList).HasForeignKey(x => x.FormatId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Format).WithMany(y => y.AttributeList).HasForeignKey(x => x.FormatId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Units).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("Attribute_Unit",
                x => x.HasOne<Unit>().WithMany().HasForeignKey("UnitId"),
                x => x.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Unit")
            );

            builder.HasMany(x => x.NodeTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("NodeType_Attribute",
                x => x.HasOne<NodeType>().WithMany().HasForeignKey("NodeTypeId"),
                x => x.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("NodeType_Attribute")
            );

            builder.HasMany(x => x.TransportTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("TransportType_Attribute",
                x => x.HasOne<TransportType>().WithMany().HasForeignKey("TransportTypeId"),
                x => x.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("TransportType_Attribute")
            );

            builder.HasMany(x => x.SimpleTypes).WithMany(y => y.AttributeList).UsingEntity<Dictionary<string, object>>("SimpleType_Attribute",
                x => x.HasOne<SimpleType>().WithMany().HasForeignKey("SimpleTypeId"),
                x => x.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("SimpleType_Attribute")
            );
        }
    }
}
