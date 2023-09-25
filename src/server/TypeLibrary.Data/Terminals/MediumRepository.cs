using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Terminals;
using TypeLibrary.Services.Terminals.Requests;

namespace TypeLibrary.Data.Terminals;

public class MediumRepository : IMediumRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlMedium> _dbSet;

    public MediumRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.Media;
    }

    public async Task<IEnumerable<RdlMedium>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlMedium?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlMedium> Create(RdlMediumRequest request)
    {
        // TODO: Implement
    }

    public async Task<bool> Delete(int id)
    {
        // TODO: Implement
    }
}