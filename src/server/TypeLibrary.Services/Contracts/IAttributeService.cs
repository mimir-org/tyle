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
        IEnumerable<AttributeLibCm> GetAttributes(Aspect aspect);
        IEnumerable<AttributeLibCm> GetAttributes();
        Task<AttributeLibCm> CreateAttribute(AttributeLibAm attributeAm);
        Task<ICollection<AttributeLibCm>> CreateAttributes(List<AttributeLibAm> attributeAmList);
        IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined();
        Task<ICollection<AttributePredefinedLibCm>> CreateAttributesPredefined(List<AttributePredefinedLibDm> attributePredefinedList);
    }
}