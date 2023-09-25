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
        throw new NotImplementedException();
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AttributeType> Create(AttributeTypeRequest request)
    {
        throw new NotImplementedException();
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
