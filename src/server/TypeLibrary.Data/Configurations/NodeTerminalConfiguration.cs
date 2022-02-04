﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Configurations
{
    public class NodeTerminalConfiguration : IEntityTypeConfiguration<NodeTerminalLibDm>
    {
        public void Configure(EntityTypeBuilder<NodeTerminalLibDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Node_Terminal");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Number).HasColumnName("Number").IsRequired().HasDefaultValue(1);
            builder.Property(p => p.ConnectorType).HasColumnName("Connector").IsRequired().HasConversion<string>();

            builder.HasOne(x => x.TerminalDm).WithMany(y => y.Nodes).HasForeignKey(x => x.TerminalTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.NodeDm).WithMany(y => y.TerminalTypes).HasForeignKey(x => x.NodeTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
