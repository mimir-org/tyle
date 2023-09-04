using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data;

public class TypeLibraryDbContext : DbContext
{
    public virtual DbSet<SymbolLibDm> Symbol { get; set; }
    public virtual DbSet<BlockTerminalLibDm> BlockTerminal { get; set; }
    //public virtual DbSet<AttributePredefinedLibDm> AttributePredefined { get; set; }
    public virtual DbSet<TerminalType> Terminal { get; set; }
    public virtual DbSet<BlockType> Block { get; set; }
    public virtual DbSet<LogLibDm> Log { get; set; }
    public virtual DbSet<AttributeType> Attribute { get; set; }
    //public virtual DbSet<UnitLibDm> Unit { get; set; }
    //public virtual DbSet<AttributeUnitLibDm> AttributeUnit { get; set; }
    public virtual DbSet<BlockAttributeTypeReference> BlockAttribute { get; set; }
    public virtual DbSet<TerminalAttributeTypeReference> TerminalAttribute { get; set; }
    //public virtual DbSet<RdsLibDm> Rds { get; set; }
    public virtual DbSet<AttributeGroup> AttributeGroup { get; set; }
    public virtual DbSet<AttributeGroupMapping> AttributeGroupMapping { get; set; }
    public virtual DbSet<ValueConstraint> ValueConstraint { get; set; }


    public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SymbolConfiguration());
        modelBuilder.ApplyConfiguration(new BlockTerminalConfiguration());
        //modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalConfiguration());
        modelBuilder.ApplyConfiguration(new BlockConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeConfiguration());
        //modelBuilder.ApplyConfiguration(new UnitConfiguration());
        //modelBuilder.ApplyConfiguration(new AttributeUnitConfiguration());
        modelBuilder.ApplyConfiguration(new BlockAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalAttributeConfiguration());
        //modelBuilder.ApplyConfiguration(new RdsConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeGroupConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeGroupMappingConfiguration());
        modelBuilder.ApplyConfiguration(new ValueConstraintConfiguration());
    }
}