using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;

namespace Tyle.Application.Attributes;

public interface IAttributeRepository : ITypeRepository<AttributeType, AttributeTypeRequest>
{
}