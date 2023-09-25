using Microsoft.EntityFrameworkCore;

namespace TypeLibrary.Data.Terminals;

public class TerminalRepository : ITerminalRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<TerminalDao> _dbSet;
    private readonly IMapper _mapper;

    private readonly IAttributeService _attributeService;

    public TerminalRepository(TyleDbContext context, IMapper mapper, IAttributeService attributeService)
    {
        _context = context;
        _dbSet = context.Terminals;
        _mapper = mapper;
        _attributeService = attributeService;
    }

    public async Task<IEnumerable<TerminalType>> GetAll()
    {
        var terminalDaos = await _dbSet.AsNoTracking()
            .Include(x => x.TerminalClassifiers)
            .ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.TerminalAttributes)
            .AsSplitQuery()
            .ToListAsync();

        return _mapper.Map<List<TerminalType>>(terminalDaos);
    }

    public async Task<TerminalType?> Get(Guid id)
    {
        var terminalDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        if (terminalDao == null)
        {
            return null;
        }

        await _context.Entry(terminalDao).Collection(x => x.TerminalClassifiers).Query().Include(x => x.Classifier).LoadAsync();
        await _context.Entry(terminalDao).Reference(x => x.Purpose).LoadAsync();
        await _context.Entry(terminalDao).Reference(x => x.Medium).LoadAsync();
        await _context.Entry(terminalDao).Collection(x => x.TerminalAttributes).LoadAsync();

        _context.Entry(terminalDao).State = EntityState.Detached;

        return _mapper.Map<TerminalType>(terminalDao);
    }

    public async Task<TerminalType> Create(TerminalType terminal)
    {
        var terminalDao = _mapper.Map<TerminalDao>(terminal);

        _dbSet.Add(terminalDao);
        await _context.SaveChangesAsync();

        return await Get(terminalDao.Id);
    }

    public Task<TerminalType> Update(TerminalType type)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
