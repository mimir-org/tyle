using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Data.Common;

public class PurposeRepository : IPurposeRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlPurpose> _dbSet;

    public PurposeRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.Purposes;
    }

    public async Task<IEnumerable<RdlPurpose>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlPurpose?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlPurpose> Create(RdlPurposeRequest request)
    {
        // TODO: Implement
    }

    public async Task<bool> Delete(int id)
    {
        // TODO: Implement
    }
}