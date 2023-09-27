using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Data.Attributes;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Terminals;

namespace TypeLibrary.Data;

public class TyleDbContext : DbContext
{
    public DbSet<RdlClassifier> Classifiers { get; set; }
    public DbSet<RdlPurpose> Purposes { get; set; }

    public DbSet<AttributeType> Attributes { get; set; }
    public DbSet<AttributeUnitJoin> AttributeUnits { get; set; }
    public DbSet<RdlPredicate> Predicates { get; set; }
    public DbSet<RdlUnit> Units { get; set; }
    public DbSet<ValueConstraint> ValueConstraints { get; set; }
    public DbSet<ValueListEntry> ValueListEntries { get; set; }

    public DbSet<RdlMedium> Media { get; set; }
    public DbSet<TerminalAttributeTypeReference> TerminalAttributes { get; set; }
    public DbSet<TerminalClassifierJoin> TerminalClassifiers { get; set; }
    public DbSet<TerminalType> Terminals { get; set; }

    public TyleDbContext(DbContextOptions<TyleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userListValueConverter = new ValueConverter<ICollection<string>, string>(
            v => string.Join(",", v),
            v => new HashSet<string>(v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)));

        var userListValueComparer = new ValueComparer<ICollection<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToHashSet());

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClassifierConfiguration());
        modelBuilder.ApplyConfiguration(new PurposeConfiguration());

        modelBuilder.ApplyConfiguration(new AttributeConfiguration(userListValueConverter, userListValueComparer));
        modelBuilder.ApplyConfiguration(new AttributeUnitJoinConfiguration());
        modelBuilder.ApplyConfiguration(new PredicateConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());
        modelBuilder.ApplyConfiguration(new ValueConstraintConfiguration());
        modelBuilder.ApplyConfiguration(new ValueListEntryConfiguration());

        modelBuilder.ApplyConfiguration(new MediumConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalAttributeTypeReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalClassifierJoinConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalConfiguration(userListValueConverter, userListValueComparer));
    }
}
