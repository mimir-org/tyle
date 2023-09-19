using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class PredicateRepository : IPredicateRepository
{
    private readonly DbContext _context;
    private readonly DbSet<PredicateDao> _dbSet;
    private readonly IMapper _mapper;

    public PredicateRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Predicates;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PredicateReference>> GetAll()
    {
        var predicateDaos = await _dbSet.ToListAsync();

        return _mapper.Map<List<PredicateReference>>(predicateDaos);
    }

    public async Task<PredicateReference?> Get(int id)
    {
        var predicateDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<PredicateReference>(predicateDao);
    }

    public async Task<PredicateReference> Create(PredicateReference predicate)
    {
        var predicateDao = _mapper.Map<PredicateDao>(predicate);
        await _dbSet.AddAsync(predicateDao);
        await _context.SaveChangesAsync();

        return await Get(predicateDao.Id);
    }

    public async Task Delete(int id)
    {
        var predicateDao = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No predicate reference with id {id} found.");
        _dbSet.Remove(predicateDao);
        await _context.SaveChangesAsync();
    }
}