using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class MediumRepository : IMediumRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlMedium> _dbSet;
    private readonly IMapper _mapper;

    public MediumRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Media;
        _mapper = mapper;
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
        var medium = _mapper.Map<RdlMedium>(request);
        _dbSet.Add(medium);
        await _context.SaveChangesAsync();

        return medium;
    }

    public async Task<bool> Delete(int id)
    {
        var medium = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (medium == null)
        {
            return false;
        }

        _dbSet.Remove(medium);
        await _context.SaveChangesAsync();

        return true;
    }
}