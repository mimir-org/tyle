using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class UnitRepository : IUnitRepository
{
    private readonly DbContext _context;
    private readonly DbSet<UnitDao> _dbSet;
    private readonly IMapper _mapper;

    public UnitRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Units;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnitReference>> GetAll()
    {
        var unitDaos = await _dbSet.AsNoTracking().ToListAsync();

        return _mapper.Map<List<UnitReference>>(unitDaos);
    }

    public async Task<UnitReference?> Get(int id)
    {
        var unitDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<UnitReference>(unitDao);
    }

    public async Task<UnitReference> Create(UnitReference unit)
    {
        var unitDao = _mapper.Map<UnitDao>(unit);
        await _dbSet.AddAsync(unitDao);
        await _context.SaveChangesAsync();

        return await Get(unitDao.Id);
    }

    public async Task Delete(int id)
    {
        var unitDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"No unit reference with id {id} found.");
        _dbSet.Remove(unitDao);
        await _context.SaveChangesAsync();
    }
}