using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<AttributeLibDm>
    {
        public void Configure(EntityTypeBuilder<AttributeLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.State).IsUnique(false);
            builder.HasIndex(x => x.FirstVersionId).IsUnique(false);
            builder.HasIndex(x => new { x.State, x.Aspect }).IsUnique(false);
            builder.ToTable("Attribute");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(7);
            builder.Property(p => p.FirstVersionId).HasColumnName("FirstVersionId").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.TypeReferences).HasColumnName("TypeReferences");
            builder.Property(p => p.Description).HasColumnName("Description").HasDefaultValue(null).HasMaxLength(511);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.SelectValuesString).HasColumnName("SelectValuesString").IsRequired(false);
            builder.Property(p => p.Select).HasColumnName("Select").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.Discipline).HasColumnName("Discipline").IsRequired().HasConversion<string>().HasMaxLength(63);
            builder.Property(p => p.QuantityDatumSpecifiedScope).HasColumnName("QuantityDatumSpecifiedScope").HasMaxLength(31);
            builder.Property(p => p.QuantityDatumSpecifiedProvenance).HasColumnName("QuantityDatumSpecifiedProvenance").HasMaxLength(31);
            builder.Property(p => p.QuantityDatumRangeSpecifying).HasColumnName("QuantityDatumRangeSpecifying").HasMaxLength(31);
            builder.Property(p => p.QuantityDatumRegularitySpecified).HasColumnName("QuantityDatumRegularitySpecified").HasMaxLength(31);
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired();
            builder.Property(p => p.Units).HasColumnName("Units");
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();

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

            builder.HasMany(x => x.Interfaces).WithMany(y => y.Attributes).UsingEntity<Dictionary<string, object>>("Attribute_Interface",
                x => x.HasOne<InterfaceLibDm>().WithMany().HasForeignKey("InterfaceId"),
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.ToTable("Attribute_Interface")
           );
        }
    }
}