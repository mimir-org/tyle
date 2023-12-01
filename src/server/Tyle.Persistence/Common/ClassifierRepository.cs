using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

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
        var classifier = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (classifier == null)
        {
            return false;
        }

        _dbSet.Remove(classifier);
        await _context.SaveChangesAsync();

        return true;
    }

    public Task<RdlClassifier> Update(int id, RdlClassifierRequest request)
    {
        throw new NotImplementedException();
    }
}