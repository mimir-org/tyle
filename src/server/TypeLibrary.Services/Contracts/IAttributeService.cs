using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeLibDm> GetAttributes(Aspect aspect);
        IEnumerable<AttributeLibDm> GetAttributes();
        Task<AttributeLibDm> CreateAttribute(AttributeLibAm attributeAm);
        Task<ICollection<AttributeLibDm>> CreateAttributes(List<AttributeLibAm> attributeAmList);
        IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined();
        Task<List<AttributePredefinedLibDm>> CreateAttributesPredefined(List<AttributePredefinedLibDm> attributePredefinedList);
    }
}