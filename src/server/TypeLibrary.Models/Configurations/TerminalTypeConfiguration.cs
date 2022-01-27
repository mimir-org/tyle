using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class TerminalTypeConfiguration : IEntityTypeConfiguration<TerminalDm>
    {
        public void Configure(EntityTypeBuilder<TerminalDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Terminal");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").IsRequired(false);

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(x => x.Attributes).WithMany(y => y.TerminalTypes).UsingEntity<Dictionary<string, object>>("Terminal_Attribute",
                x => x.HasOne<AttributeDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.HasOne<TerminalDm>().WithMany().HasForeignKey("TerminalId"),
                x => x.ToTable("Terminal_Attribute")
            );
        }
    }
}
