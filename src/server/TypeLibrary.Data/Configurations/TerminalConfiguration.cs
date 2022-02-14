using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class TerminalConfiguration : IEntityTypeConfiguration<TerminalLibDm>
    {
        public void Configure(EntityTypeBuilder<TerminalLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Terminal");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);
            builder.Property(p => p.ParentId).HasColumnName("ParentId").IsRequired(false);

            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(x => x.Attributes).WithMany(y => y.Terminals).UsingEntity<Dictionary<string, object>>("Terminal_Attribute",
                x => x.HasOne<AttributeLibDm>().WithMany().HasForeignKey("AttributeId"),
                x => x.HasOne<TerminalLibDm>().WithMany().HasForeignKey("TerminalId"),
                x => x.ToTable("Terminal_Attribute")
            );
        }
    }
}
