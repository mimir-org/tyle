using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeType> _dbSet;
    private readonly IMapper _mapper;

    public AttributeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Attributes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttributeType>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AttributeType> Create(AttributeTypeRequest request)
    {
        var attribute = _mapper.Map<AttributeType>(request);
        attribute.Version = "1.0";
        attribute.CreatedBy = "";
        attribute.CreatedOn = DateTime.Now;
        attribute.LastUpdateOn = attribute.CreatedOn;

        _dbSet.Add(attribute);
        await _context.SaveChangesAsync();
        
        return attribute;
    }

    public async Task<AttributeType?> Update(Guid id, AttributeTypeRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
