using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class PredicateRepository : IPredicateRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlPredicate> _dbSet;
    private readonly IMapper _mapper;

    public PredicateRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Predicates;
        _mapper = mapper;
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
        var predicate = _mapper.Map<RdlPredicate>(request);
        _dbSet.Add(predicate);
        await _context.SaveChangesAsync();

        return predicate;
    }

    public async Task Create(IEnumerable<RdlPredicateRequest> requests, ReferenceSource source)
    {
        var predicates = _mapper.Map<IEnumerable<RdlPredicate>>(requests).ToList();

        foreach (var predicate in predicates)
        {
            predicate.Source = source;
        }

        _dbSet.AddRange(predicates);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var predicate = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (predicate == null)
        {
            return false;
        }

        _dbSet.Remove(predicate);
        await _context.SaveChangesAsync();

        return true;
    }
}