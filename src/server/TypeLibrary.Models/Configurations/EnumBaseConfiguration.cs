using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Configurations
{
    public class EnumBaseConfiguration: IEntityTypeConfiguration<EnumBase>
    {
        public void Configure(EntityTypeBuilder<EnumBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Enum");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired().ValueGeneratedNever();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.Aspect).HasColumnName("Aspect").IsRequired().HasConversion<string>();
            builder.Property(p => p.ParentId).HasColumnName("ParentId").IsRequired(false);

            builder.Property(p => p.InternalType).HasColumnName("InternalType").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(p => p.SemanticReference).HasColumnName("SemanticReference").IsRequired(false);
            
            builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
