using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Common;
using Tyle.Core.Terminals;
using Tyle.Persistence.Attributes;
using Tyle.Persistence.Blocks;
using Tyle.Persistence.Common;
using Tyle.Persistence.Terminals;

namespace Tyle.Persistence;

public class TyleDbContext : DbContext
{
    public DbSet<RdlClassifier> Classifiers { get; set; }
    public DbSet<RdlPurpose> Purposes { get; set; }

    public DbSet<AttributeType> Attributes { get; set; }
    public DbSet<AttributeGroup> AttributeGroups { get; set; }
    public DbSet<AttributeGroupAttributeJoin> AttributeGroupAttributes { get; set; }
    public DbSet<AttributeUnitJoin> AttributeUnits { get; set; }
    public DbSet<RdlPredicate> Predicates { get; set; }
    public DbSet<RdlUnit> Units { get; set; }
    public DbSet<ValueConstraint> ValueConstraints { get; set; }
    public DbSet<ValueListEntry> ValueListEntries { get; set; }

    public DbSet<RdlMedium> Media { get; set; }
    public DbSet<TerminalAttributeTypeReference> TerminalAttributes { get; set; }
    public DbSet<TerminalClassifierJoin> TerminalClassifiers { get; set; }
    public DbSet<TerminalType> Terminals { get; set; }

    public DbSet<BlockType> Blocks { get; set; }
    public DbSet<EngineeringSymbol> EngineeringSymbols { get; set; }
    public DbSet<ConnectionPoint> ConnectionPoints { get; set; }
    public DbSet<BlockAttributeTypeReference> BlockAttributes { get; set; }
    public DbSet<BlockClassifierJoin> BlockClassifiers { get; set; }
    public DbSet<BlockTerminalTypeReference> BlockTerminals { get; set; }

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
        modelBuilder.ApplyConfiguration(new AttributeGroupConfiguration(userListValueConverter, userListValueComparer));
        modelBuilder.ApplyConfiguration(new AttributeGroupAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeUnitConfiguration());
        modelBuilder.ApplyConfiguration(new PredicateConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());
        modelBuilder.ApplyConfiguration(new ValueConstraintConfiguration());
        modelBuilder.ApplyConfiguration(new ValueListEntryConfiguration());

        modelBuilder.ApplyConfiguration(new MediumConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalClassifierConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalConfiguration(userListValueConverter, userListValueComparer));

        modelBuilder.ApplyConfiguration(new BlockConfiguration(userListValueConverter, userListValueComparer));
        modelBuilder.ApplyConfiguration(new SymbolConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionPointConfiguration());
        modelBuilder.ApplyConfiguration(new BlockAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new BlockClassifierConfiguration());
        modelBuilder.ApplyConfiguration(new BlockTerminalConfiguration());
    }
}