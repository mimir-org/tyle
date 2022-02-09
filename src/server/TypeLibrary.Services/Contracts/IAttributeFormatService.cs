using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeFormatService
    {
        Task<IEnumerable<AttributeFormatLibAm>> GetAttributeFormats();
        Task<AttributeFormatLibAm> UpdateAttributeFormat(AttributeFormatLibAm dataAm);
        Task<AttributeFormatLibAm> CreateAttributeFormat(AttributeFormatLibAm dataAm);
        Task CreateAttributeFormats(List<AttributeFormatLibAm> dataAm);
    }
}