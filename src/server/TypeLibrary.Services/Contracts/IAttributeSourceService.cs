using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeSourceService
    {
        Task<IEnumerable<AttributeSourceLibAm>> GetAttributeSources();
        Task<AttributeSourceLibAm> UpdateAttributeSource(AttributeSourceLibAm dataAm);
        Task<AttributeSourceLibAm> CreateAttributeSource(AttributeSourceLibAm dataAm);
        Task CreateAttributeSources(List<AttributeSourceLibAm> dataAm);
    }
}