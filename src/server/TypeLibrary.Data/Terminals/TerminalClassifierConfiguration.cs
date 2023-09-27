using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Data.Terminals;

public class TerminalClassifierConfiguration : IEntityTypeConfiguration<TerminalClassifierJoin>
{
    public void Configure(EntityTypeBuilder<TerminalClassifierJoin> builder)
    {
        builder.ToTable("Terminal_Classifier");

        builder.HasKey(x => new { x.TerminalId, x.ClassifierId });

        builder
            .HasOne(e => e.Terminal)
            .WithMany(e => e.Classifiers)
            .HasForeignKey(e => e.TerminalId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(e => e.Classifier)
            .WithMany()
            .HasForeignKey(e => e.ClassifierId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}