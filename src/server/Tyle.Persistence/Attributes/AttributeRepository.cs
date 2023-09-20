using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly DbContext _context;
    private readonly DbSet<AttributeDao> _dbSet;
    private readonly IMapper _mapper;

    public AttributeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Attributes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttributeType>> GetAll()
    {
        var attributeDaos = await _dbSet
            .Include(x => x.Predicate)
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint)
            .AsSplitQuery()
            .ToListAsync();

        return _mapper.Map<List<AttributeType>>(attributeDaos);
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        var attributeDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        if (attributeDao == null)
        {
            return null;
        }

        await _context.Entry(attributeDao).Reference(x => x.Predicate).LoadAsync();
        await _context.Entry(attributeDao).Collection(x => x.AttributeUnits).Query().Include(x => x.Unit).LoadAsync();
        await _context.Entry(attributeDao).Reference(x => x.ValueConstraint).LoadAsync();

        return _mapper.Map<AttributeType>(attributeDao);
    }

    public Task<AttributeType> Create(AttributeType type)
    {
        throw new NotImplementedException();
    }

    public Task<AttributeType> Update(AttributeType type)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
