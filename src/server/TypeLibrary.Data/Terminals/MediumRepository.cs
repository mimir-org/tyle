using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Terminals;
using TypeLibrary.Services.Terminals.Requests;

namespace TypeLibrary.Data.Terminals;

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
        try
        {
            var mediumStub = new RdlMedium { Id = id };
            _dbSet.Remove(mediumStub);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

        return true;
    }
}