using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeAspectService
    {
        Task<IEnumerable<AttributeAspectLibAm>> GetAttributeAspects();
        Task<AttributeAspectLibAm> UpdateAttributeAspect(AttributeAspectLibAm dataAm);
        Task<AttributeAspectLibAm> CreateAttributeAspect(AttributeAspectLibAm dataAm);
        Task CreateAttributeAspects(List<AttributeAspectLibAm> dataAm);
    }
}