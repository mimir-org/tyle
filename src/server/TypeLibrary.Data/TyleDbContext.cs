using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;

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
}
