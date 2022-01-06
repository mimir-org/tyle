using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data;
using Mb.Models.Data.TypeEditor;
using Mb.Models.Enums;

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
