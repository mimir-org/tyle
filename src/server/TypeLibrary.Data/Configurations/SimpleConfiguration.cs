﻿using System.Collections.Generic;
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
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.Iri).HasColumnName("Iri").IsRequired(false);

            builder.HasMany(x => x.Nodes).WithMany(y => y.Simples).UsingEntity<Dictionary<string, object>>("Simple_Node",
                x => x.HasOne<NodeLibDm>().WithMany().HasForeignKey("NodeId"),
                x => x.HasOne<SimpleLibDm>().WithMany().HasForeignKey("SimpleId"),
                x => x.ToTable("Simple_Node")
            );
        }
    }
}