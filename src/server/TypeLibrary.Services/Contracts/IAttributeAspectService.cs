using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeAspectService
    {
        Task<IEnumerable<AttributeAspectLibCm>> GetAttributeAspects();
        Task<AttributeAspectLibCm> UpdateAttributeAspect(AttributeAspectLibAm dataAm, string id);
        Task<AttributeAspectLibCm> CreateAttributeAspect(AttributeAspectLibAm dataAm);
        Task CreateAttributeAspects(List<AttributeAspectLibAm> dataAm, bool createdBySystem = false);
    }
}