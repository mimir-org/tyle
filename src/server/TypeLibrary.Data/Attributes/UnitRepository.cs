using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class UnitRepository : IUnitRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlUnit> _dbSet;

    public UnitRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.Units;
    }

    public async Task<IEnumerable<RdlUnit>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlUnit?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlUnit> Create(RdlUnitRequest request)
    {
        // TODO: Implement
    }

    public async Task<bool> Delete(int id)
    {
        // TODO: Implement
    }
}