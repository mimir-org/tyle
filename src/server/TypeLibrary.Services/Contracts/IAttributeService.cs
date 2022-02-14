using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

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