using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IFormatService
    {
        Task<IEnumerable<FormatLibAm>> GetFormats();
        Task<FormatLibAm> UpdateFormat(FormatLibAm dataAm);
        Task<FormatLibAm> CreateFormat(FormatLibAm dataAm);
        Task CreateFormats(List<FormatLibAm> dataAm);
    }
}