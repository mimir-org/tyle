using Microsoft.EntityFrameworkCore;
using Tyle.Persistence.Attributes;
using Tyle.Persistence.Common;
using Tyle.Persistence.Terminals;

namespace Tyle.Persistence;

public class TyleDbContext : DbContext
{
    public DbSet<ClassifierDao> Classifiers { get; set; }
    public DbSet<MediumDao> Media { get; set; }
    public DbSet<PredicateDao> Predicates { get; set; }
    public DbSet<PurposeDao> Purposes { get; set; }
    public DbSet<UnitDao> Units { get; set; }

    public TyleDbContext(DbContextOptions<TyleDbContext> options) : base(options)
    {
    }
}
