using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class EdgeConfiguration : IEntityTypeConfiguration<Edge>
    {
        public void Configure(EntityTypeBuilder<Edge> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Edge");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired();
            builder.Property(p => p.FromConnectorId).HasColumnName("FromConnectorId").IsRequired();
            builder.Property(p => p.ToConnectorId).HasColumnName("ToConnectorId").IsRequired();
            builder.Property(p => p.FromNodeId).HasColumnName("FromNodeId").IsRequired();
            builder.Property(p => p.ToNodeId).HasColumnName("ToNodeId").IsRequired();
            builder.Property(p => p.FromConnectorIri).HasColumnName("FromConnectorIri").IsRequired();
            builder.Property(p => p.ToConnectorIri).HasColumnName("ToConnectorIri").IsRequired();
            builder.Property(p => p.FromNodeIri).HasColumnName("FromNodeIri").IsRequired();
            builder.Property(p => p.ToNodeIri).HasColumnName("ToNodeIri").IsRequired();

            builder.Property(p => p.TransportId).HasColumnName("TransportId").IsRequired(false);
            builder.Property(p => p.InterfaceId).HasColumnName("InterfaceId").IsRequired(false);

            builder.HasOne(x => x.FromNode).WithMany(y => y.FromEdges).HasForeignKey(x => x.FromNodeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ToNode).WithMany(y => y.ToEdges).HasForeignKey(x => x.ToNodeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FromConnector).WithMany(y => y.FromEdges).HasForeignKey(x => x.FromConnectorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ToConnector).WithMany(y => y.ToEdges).HasForeignKey(x => x.ToConnectorId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Transport).WithMany(y => y.Edges).HasForeignKey(x => x.TransportId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Interface).WithMany(y => y.Edges).HasForeignKey(x => x.InterfaceId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.MasterProjectId).HasColumnName("MasterProjectId").IsRequired();
            builder.Property(p => p.MasterProjectIri).HasColumnName("MasterProjectIri").IsRequired();

            builder.Property(p => p.IsLocked).HasColumnName("IsLocked").IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsLockedStatusBy).HasColumnName("IsLockedStatusBy").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.IsLockedStatusDate).HasColumnName("IsLockedStatusDate").IsRequired(false).HasDefaultValue(null);
        }
    }
}
