using Microsoft.EntityFrameworkCore;
using Tyle.Persistence.Attributes;
using Tyle.Persistence.Common;
using Tyle.Persistence.Terminals;

namespace Tyle.Persistence;

public class TyleDbContext : DbContext
{
    public DbSet<ClassifierDao> Classifiers { get; set; }
    public DbSet<PurposeDao> Purposes { get; set; }

    public DbSet<AttributeDao> Attributes { get; set; }
    public DbSet<AttributeUnitDao> AttributeUnits { get; set; }
    public DbSet<PredicateDao> Predicates { get; set; }
    public DbSet<UnitDao> Units { get; set; }
    public DbSet<ValueConstraintDao> ValueConstraints { get; set; }
    public DbSet<ValueListEntriesDao> ValueListEntries { get; set; }

    public DbSet<MediumDao> Media { get; set; }

    public TyleDbContext(DbContextOptions<TyleDbContext> options) : base(options)
    {
    }
}
