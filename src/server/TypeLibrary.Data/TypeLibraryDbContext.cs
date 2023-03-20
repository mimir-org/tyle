using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Configurations;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data;

public class TypeLibraryDbContext : DbContext
{
    public virtual DbSet<SymbolLibDm> Symbol { get; set; }
    public virtual DbSet<NodeTerminalLibDm> TerminalNode { get; set; }
    public virtual DbSet<AttributePredefinedLibDm> AttributePredefined { get; set; }
    public virtual DbSet<TerminalLibDm> Terminal { get; set; }
    public virtual DbSet<NodeLibDm> Node { get; set; }
    public virtual DbSet<LogLibDm> Log { get; set; }

    public TypeLibraryDbContext(DbContextOptions<TypeLibraryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
        modelBuilder.ApplyConfiguration(new SymbolConfiguration());
        modelBuilder.ApplyConfiguration(new NodeTerminalConfiguration());
        modelBuilder.ApplyConfiguration(new AttributePredefinedConfiguration());
        modelBuilder.ApplyConfiguration(new TerminalConfiguration());
        modelBuilder.ApplyConfiguration(new NodeConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
    }
}