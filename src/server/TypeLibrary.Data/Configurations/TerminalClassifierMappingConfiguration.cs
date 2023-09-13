using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Configurations;

public class TerminalClassifierMappingConfiguration : IEntityTypeConfiguration<TerminalClassifierMapping>
{
    public void Configure(EntityTypeBuilder<TerminalClassifierMapping> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("TerminalClassifierMapping");
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();

        builder.HasOne(e => e.Terminal).WithMany(e => e.Classifiers).HasForeignKey(e => e.TerminalId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(e => e.Classifier).WithMany().HasForeignKey(e => e.ClassifierId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    }
}