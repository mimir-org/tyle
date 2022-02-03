using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ISourceService
    {
        Task<IEnumerable<SourceAm>> GetSources();
        Task<SourceAm> UpdateSource(SourceAm dataAm);
        Task<SourceAm> CreateSource(SourceAm dataAm);
        Task CreateSources(List<SourceAm> dataAm);
    }
}