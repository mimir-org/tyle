using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Persistence.Attributes;

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

    public async Task<RdlPurpose> Update(int id, RdlPurposeRequest request)
    {
        var purpose = await _dbSet.AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (purpose == null)
        {
            return null;
        }

        purpose.Name = request.Name;
        purpose.Description = request.Description;

        await _context.SaveChangesAsync();

        return await Get(id);
    }
}