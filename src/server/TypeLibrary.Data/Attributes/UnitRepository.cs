using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class UnitRepository : IUnitRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlUnit> _dbSet;
    private readonly IMapper _mapper;

    public UnitRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Units;
        _mapper = mapper;
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
        var unit = _mapper.Map<RdlUnit>(request);
        _dbSet.Add(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var unitStub = new RdlUnit { Id = id };
            _dbSet.Remove(unitStub);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

        return true;
    }
}