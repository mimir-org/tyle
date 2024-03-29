using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

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

    public async Task Create(IEnumerable<RdlPurposeRequest> requests, ReferenceSource source)
    {
        var purposes = _mapper.Map<IEnumerable<RdlPurpose>>(requests).ToList();

        foreach (var purpose in purposes)
        {
            purpose.Source = source;
        }

        _dbSet.AddRange(purposes);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var purpose = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (purpose == null)
        {
            return false;
        }

        _dbSet.Remove(purpose);
        await _context.SaveChangesAsync();

        return true;
    }
}