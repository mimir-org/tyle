using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class ConnectorConfiguration : IEntityTypeConfiguration<Connector>
    {
        public void Configure(EntityTypeBuilder<Connector> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Connector");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Type).HasColumnName("Type").IsRequired().HasConversion<string>();
            builder.Property(p => p.Visible).HasColumnName("Visible").IsRequired().HasConversion<string>();
            builder.Property(p => p.SemanticReference).HasColumnName("SemanticReference").IsRequired(false);
            builder.Property(p => p.NodeId).HasColumnName("NodeId").IsRequired(false);
            builder.Property(p => p.NodeIri).HasColumnName("NodeIri").IsRequired(false);
            builder.Property(p => p.IsRequired).HasColumnName("IsRequired").IsRequired().HasDefaultValue(false);

            builder.HasOne(x => x.Node).WithMany(y => y.Connectors).HasForeignKey(x => x.NodeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
