using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeTypeService
    {
        Task<IEnumerable<AttributeTypeLibAm>> GetAttributeTypes();
        Task<AttributeTypeLibAm> UpdateAttributeType(AttributeTypeLibAm dataAm);
        Task<AttributeTypeLibAm> CreateAttributeType(AttributeTypeLibAm dataAm);
        Task CreateAttributeTypes(List<AttributeTypeLibAm> dataAm);
    }
}