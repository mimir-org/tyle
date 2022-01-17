using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeTypeService
    {
        IEnumerable<AttributeType> GetAttributeTypes(Aspect aspect);
        Task<AttributeType> CreateAttributeType(AttributeTypeAm createAttributeType);
        Task<ICollection<AttributeType>> CreateAttributeTypes(List<AttributeTypeAm> attributeTypes);
        IEnumerable<PredefinedAttributeAm> GetPredefinedAttributes();
        Task<List<PredefinedAttribute>> CreatePredefinedAttributes(List<PredefinedAttribute> attributes);
    }
}
