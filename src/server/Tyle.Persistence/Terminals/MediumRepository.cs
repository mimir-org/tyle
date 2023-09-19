using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Terminals;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class MediumRepository : IMediumRepository
{
    private readonly DbContext _context;
    private readonly DbSet<MediumDao> _dbSet;
    private readonly IMapper _mapper;

    public MediumRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Media;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MediumReference>> GetAll()
    {
        var mediumDaos = await _dbSet.ToListAsync();

        return _mapper.Map<List<MediumReference>>(mediumDaos);
    }

    public async Task<MediumReference?> Get(int id)
    {
        var mediumDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<MediumReference>(mediumDao);
    }

    public async Task<MediumReference> Create(MediumReference medium)
    {
        var mediumDao = _mapper.Map<MediumDao>(medium);
        await _dbSet.AddAsync(mediumDao);
        await _context.SaveChangesAsync();

        return await Get(mediumDao.Id);
    }

    public async Task Delete(int id)
    {
        var mediumDao = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No medium reference with id {id} found.");
        _dbSet.Remove(mediumDao);
        await _context.SaveChangesAsync();
    }
}