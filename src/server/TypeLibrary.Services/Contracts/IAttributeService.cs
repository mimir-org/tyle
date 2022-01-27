using System.Collections.Generic;
using System.Threading.Tasks;

using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeDm> GetAttributes(Aspect aspect);
        Task<AttributeDm> CreateAttribute(AttributeAm attributeAm);
        Task<ICollection<AttributeDm>> CreateAttributes(List<AttributeAm> attributeAmList);
        IEnumerable<PredefinedAttributeCm> GetPredefinedAttributes();
        Task<List<PredefinedAttributeDm>> CreatePredefinedAttributes(List<PredefinedAttributeDm> predefinedAttributeList);
    }
}