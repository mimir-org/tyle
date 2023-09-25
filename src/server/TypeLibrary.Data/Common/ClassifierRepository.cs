using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Data.Common;

public class ClassifierRepository : IClassifierRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlClassifier> _dbSet;

    public ClassifierRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.Classifiers;
    }

    public async Task<IEnumerable<RdlClassifier>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlClassifier?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlClassifier> Create(RdlClassifierRequest request)
    {
        // TODO: Implement
    }

    public async Task<bool> Delete(int id)
    {
        // TODO: Implement
    }
}
