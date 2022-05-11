using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class SimpleConfiguration : IEntityTypeConfiguration<SimpleLibDm>
    {
        public void Configure(EntityTypeBuilder<SimpleLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Simple");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().HasMaxLength(127);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(63);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false).HasMaxLength(511);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false).HasMaxLength(255);
            builder.Property(p => p.ContentReferences).HasColumnName("ContentReferences");
            builder.Property(p => p.Deleted).HasColumnName("Deleted").IsRequired().HasDefaultValue(0);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasMaxLength(31);
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null).HasMaxLength(31);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);

            builder.HasMany(x => x.Nodes).WithMany(y => y.Simples).UsingEntity<Dictionary<string, object>>("Simple_Node",
                x => x.HasOne<NodeLibDm>().WithMany().HasForeignKey("NodeId"),
                x => x.HasOne<SimpleLibDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.ToTable("Simple_Node")
            );
        }
    }
}