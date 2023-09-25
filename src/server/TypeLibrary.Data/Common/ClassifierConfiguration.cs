using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Data.Common;

public class ClassifierConfiguration : IEntityTypeConfiguration<RdlClassifier>
{
    public void Configure(EntityTypeBuilder<RdlClassifier> builder)
    {
        builder.ToTable("Classifier");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Iri).IsRequired().HasConversion<string>();
        builder.Property(x => x.Source).IsRequired().HasConversion<string>();
    }
}