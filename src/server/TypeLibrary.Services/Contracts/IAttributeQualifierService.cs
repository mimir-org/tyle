using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeQualifierService
    {
        Task<IEnumerable<AttributeQualifierLibCm>> GetAttributeQualifiers();
        Task<AttributeQualifierLibCm> UpdateAttributeQualifier(AttributeQualifierLibAm dataAm, int id);
        Task<AttributeQualifierLibCm> CreateAttributeQualifier(AttributeQualifierLibAm dataAm);
        Task CreateAttributeQualifiers(List<AttributeQualifierLibAm> dataAm, bool createdBySystem = false);
    }
}