using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeLibCm> GetAttributes(Aspect aspect);
        Task<AttributeLibCm> GetAttribute(string id);
        Task<AttributeLibCm> CreateAttribute(AttributeLibAm attributeAm);
        Task CreateAttributes(List<AttributeLibAm> attributeAmList, bool createdBySystem = false);
        IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined();
        Task CreateAttributesPredefined(List<AttributePredefinedLibAm> attributePredefinedList, bool createdBySystem = false);
    }
}