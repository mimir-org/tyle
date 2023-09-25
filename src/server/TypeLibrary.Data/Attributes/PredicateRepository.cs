using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class PredicateRepository : IPredicateRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlPredicate> _dbSet;

    public PredicateRepository(TyleDbContext context)
    {
        _context = context;
        _dbSet = context.Predicates;
    }

    public async Task<IEnumerable<RdlPredicate>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlPredicate?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlPredicate> Create(RdlPredicateRequest request)
    {
        // TODO: Implement
    }

    public async Task<bool> Delete(int id)
    {
        // TODO: Implement
    }
}