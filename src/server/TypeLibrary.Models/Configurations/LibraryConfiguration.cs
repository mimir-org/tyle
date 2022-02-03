using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Configurations
{
    public class LibraryConfiguration : IEntityTypeConfiguration<TypeDm>

    {
        public void Configure(EntityTypeBuilder<TypeDm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Type");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Version).HasColumnName("Version").IsRequired();
            builder.Property(p => p.TypeId).HasColumnName("TypeId").IsRequired();
            builder.Property(p => p.SemanticReference).HasColumnName("SemanticReference").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.PurposeId).HasColumnName("PurposeId").IsRequired(false);
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.StatusId).HasColumnName("StatusId").IsRequired().HasDefaultValue("4590637F39B6BA6F39C74293BE9138DF");
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.Updated).HasColumnName("Updated").IsRequired(false).HasDefaultValue(null);
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasDefaultValue("Unknown");
            builder.Property(p => p.Created).HasColumnName("Created").IsRequired().HasDefaultValue(DateTime.MinValue.ToUniversalTime());

            builder.HasOne(x => x.PurposeDm).WithMany(y => y.LibraryTypes).HasForeignKey(x => x.PurposeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RdsDm).WithMany(y => y.LibraryTypes).HasForeignKey(x => x.RdsId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Categories).WithMany(y => y.Types).UsingEntity<Dictionary<string, object>>("Type_Category",
                x => x.HasOne<CategoryDm>().WithMany().HasForeignKey("CategoryId"),
                x => x.HasOne<TypeDm>().WithMany().HasForeignKey("TypeId"),
                x => x.ToTable("Type_Category"));
        }
    }
}
