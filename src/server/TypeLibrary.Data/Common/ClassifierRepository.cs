using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Data.Common;

public class ClassifierRepository : IClassifierRepository
{
    private readonly DbContext _context;
    private readonly DbSet<RdlClassifier> _dbSet;
    private readonly IMapper _mapper;

    public ClassifierRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Classifiers;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RdlClassifier>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<RdlClassifier?> Get(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RdlClassifier> Create(RdlClassifierRequest request)
    {
        var classifier = _mapper.Map<RdlClassifier>(request);
        _dbSet.Add(classifier);
        await _context.SaveChangesAsync();

        return classifier;
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
