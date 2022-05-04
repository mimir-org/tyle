using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;
using Mimirorg.Common.Converters;

namespace TypeLibrary.Data.Configurations
{
    public class NodeConfiguration : IEntityTypeConfiguration<NodeLibDm>
    {
        public void Configure(EntityTypeBuilder<NodeLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Node");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasDefaultValue(null).HasMaxLength(255);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.RdsCode).HasColumnName("RdsCode").IsRequired().HasMaxLength(127);
            builder.Property(p => p.RdsName).HasColumnName("RdsName").IsRequired().HasMaxLength(127);
            builder.Property(p => p.PurposeName).HasColumnName("PurposeName").IsRequired().HasMaxLength(127);
            builder.Property(p => p.PurposeDiscipline).HasColumnName("PurposeDiscipline").IsRequired().HasMaxLength(127);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").HasMaxLength(127);
            builder.Property(p => p.Version).HasColumnName("Version").IsRequired().HasMaxLength(7);
            builder.Property(p => p.FirstVersionId).HasColumnName("FirstVersionId").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.State).HasColumnName("State").IsRequired().HasConversion<string>().HasMaxLength(31);
            builder.Property(p => p.CompanyId).HasColumnName("CompanyId").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false).HasDefaultValue(null).HasMaxLength(511);
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null).HasMaxLength(63);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired().HasDefaultValue(DateTime.MinValue.ToUniversalTime());
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasDefaultValue("Unknown").HasMaxLength(63);
            builder.Property(p => p.Symbol).HasColumnName("Symbol").HasMaxLength(127);
            builder.Property(p => p.AttributeAspectIri).HasColumnName("AttributeAspectIri").HasMaxLength(127);
            builder.Property(p => p.SelectedAttributePredefined).HasJsonConversion();

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}