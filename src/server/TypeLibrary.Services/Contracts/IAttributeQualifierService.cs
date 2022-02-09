using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeQualifierService
    {
        Task<IEnumerable<AttributeQualifierLibAm>> GetAttributeQualifiers();
        Task<AttributeQualifierLibAm> UpdateAttributeQualifier(AttributeQualifierLibAm dataAm);
        Task<AttributeQualifierLibAm> CreateAttributeQualifier(AttributeQualifierLibAm dataAm);
        Task CreateAttributeQualifiers(List<AttributeQualifierLibAm> dataAm);
    }
}