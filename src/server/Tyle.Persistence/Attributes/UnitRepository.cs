using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

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

    public async Task Create(IEnumerable<RdlUnitRequest> requests, ReferenceSource source)
    {
        var units = _mapper.Map<IEnumerable<RdlUnit>>(requests).ToList();

        foreach (var unit in units)
        {
            unit.Source = source;
        }

        _dbSet.AddRange(units);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var unit = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (unit == null)
        {
            return false;
        }

        _dbSet.Remove(unit);
        await _context.SaveChangesAsync();

        return true;
    }
}