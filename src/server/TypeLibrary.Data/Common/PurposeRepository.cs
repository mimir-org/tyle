using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Data.Common;

public class PurposeRepository : IPurposeRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlPurpose> _dbSet;
    private readonly IMapper _mapper;

    public PurposeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Purposes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RdlPurpose>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlPurpose?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlPurpose> Create(RdlPurposeRequest request)
    {
        var purpose = _mapper.Map<RdlPurpose>(request);
        _dbSet.Add(purpose);
        await _context.SaveChangesAsync();

        return purpose;
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}