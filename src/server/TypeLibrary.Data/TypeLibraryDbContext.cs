using Microsoft.EntityFrameworkCore;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Configurations;

namespace TypeLibrary.Data;

public class TypeLibraryDbContext : DbContext
{
    public virtual DbSet<AttributeType> Attribute { get; set; }
    public virtual DbSet<BlockType> Block { get; set; }
    public virtual DbSet<TerminalType> Terminal { get; set; }

    public virtual DbSet<BlockTerminalTypeReference> BlockTerminal { get; set; }
    public virtual DbSet<BlockAttributeTypeReference> BlockAttribute { get; set; }
    public virtual DbSet<TerminalAttributeTypeReference> TerminalAttribute { get; set; }

    public virtual DbSet<ClassifierReference> Classifier { get; set; }
    public virtual DbSet<MediumReference> Medium { get; set; }
    public virtual DbSet<PredicateReference> Predicate { get; set; }
    public virtual DbSet<PurposeReference> Purpose { get; set; }
    public virtual DbSet<UnitReference> Unit { get; set; }
    public virtual DbSet<ValueConstraint> ValueConstraint { get; set; }

    public virtual DbSet<LogLibDm> Log { get; set; }
    public virtual DbSet<SymbolLibDm> Symbol { get; set; }

    public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AttributeConfiguration());
        modelBuilder.ApplyConfiguration(new BlockConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalConfiguration());

        modelBuilder.ApplyConfiguration(new BlockTerminalConfiguration());
        modelBuilder.ApplyConfiguration(new BlockAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalAttributeConfiguration());

        modelBuilder.ApplyConfiguration(new ClassifierConfiguration());
        modelBuilder.ApplyConfiguration(new MediumConfiguration());
        modelBuilder.ApplyConfiguration(new PredicateConfiguration());
        modelBuilder.ApplyConfiguration(new PurposeConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());
        modelBuilder.ApplyConfiguration(new ValueConstraintConfiguration());

        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new SymbolConfiguration());
    }
}