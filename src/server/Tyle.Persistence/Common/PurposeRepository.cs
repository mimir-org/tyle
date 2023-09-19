using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Common;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class PurposeRepository : IPurposeRepository
{
    private readonly DbContext _context;
    private readonly DbSet<PurposeDao> _dbSet;
    private readonly IMapper _mapper;

    public PurposeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Purposes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PurposeReference>> GetAll()
    {
        var purposeDaos = await _dbSet.ToListAsync();

        return _mapper.Map<List<PurposeReference>>(purposeDaos);
    }

    public async Task<PurposeReference?> Get(int id)
    {
        var purposeDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<PurposeReference>(purposeDao);
    }

    public async Task<PurposeReference> Create(PurposeReference purpose)
    {
        var purposeDao = _mapper.Map<PurposeDao>(purpose);
        await _dbSet.AddAsync(purposeDao);
        await _context.SaveChangesAsync();

        return await Get(purposeDao.Id);
    }

    public async Task Delete(int id)
    {
        var purposeDao = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No purpose reference with id {id} found.");
        _dbSet.Remove(purposeDao);
        await _context.SaveChangesAsync();
    }
}