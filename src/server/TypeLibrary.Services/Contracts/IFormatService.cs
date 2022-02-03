using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IFormatService
    {
        Task<IEnumerable<FormatAm>> GetFormats();
        Task<FormatAm> UpdateFormat(FormatAm dataAm);
        Task<FormatAm> CreateFormat(FormatAm dataAm);
        Task CreateFormats(List<FormatAm> dataAm);
    }
}