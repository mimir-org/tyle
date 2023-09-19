using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Common;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class ClassifierRepository : IClassifierRepository
{
    private readonly DbContext _context;
    private readonly DbSet<ClassifierDao> _dbSet;
    private readonly IMapper _mapper;

    protected ClassifierRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Classifiers;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClassifierReference>> GetAll()
    {
        var classifierDaos = await _dbSet.ToListAsync();

        return _mapper.Map<List<ClassifierReference>>(classifierDaos);
    }

    public async Task<ClassifierReference?> Get(int id)
    {
        var classifierDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<ClassifierReference>(classifierDao);
    }

    public async Task<ClassifierReference> Create(ClassifierReference classifier)
    {
        var classifierDao = _mapper.Map<ClassifierDao>(classifier);
        await _dbSet.AddAsync(classifierDao);
        await _context.SaveChangesAsync();

        return await Get(classifierDao.Id);
    }

    public async Task Delete(int id)
    {
        var classifierDao = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No classifier reference with id {id} found.");
        _dbSet.Remove(classifierDao);
        await _context.SaveChangesAsync();
    }
}
