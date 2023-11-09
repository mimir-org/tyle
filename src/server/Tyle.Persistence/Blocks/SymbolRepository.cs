using Microsoft.EntityFrameworkCore;
using Tyle.Application.Blocks;
using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class SymbolRepository : ISymbolRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<EngineeringSymbol> _dbSet;

    public SymbolRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.EngineeringSymbols;
    }

    public async Task<IEnumerable<EngineeringSymbol>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.ConnectionPoints)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<EngineeringSymbol?> Get(int id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.ConnectionPoints)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}