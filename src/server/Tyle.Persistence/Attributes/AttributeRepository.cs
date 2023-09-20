using Tyle.Application.Attributes;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeRepository : IAttributeRepository
{
    public Task<IEnumerable<AttributeType>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<AttributeType?> Get(Guid id)
    {
        throw new NotImplementedException();
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
