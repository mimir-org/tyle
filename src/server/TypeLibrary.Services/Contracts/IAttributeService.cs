using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;
using PredefinedAttribute = TypeLibrary.Models.Application.PredefinedAttribute;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<Attribute> GetAttributes(Aspect aspect);
        Task<Attribute> CreateAttribute(AttributeAm attributeAm);
        Task<ICollection<Attribute>> CreateAttributes(List<AttributeAm> attributeAmList);
        IEnumerable<PredefinedAttribute> GetPredefinedAttributes();
        Task<List<Models.Data.PredefinedAttribute>> CreatePredefinedAttributes(List<Models.Data.PredefinedAttribute> predefinedAttributeList);
    }
}