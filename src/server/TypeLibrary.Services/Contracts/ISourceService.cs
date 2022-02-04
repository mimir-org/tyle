using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ISourceService
    {
        Task<IEnumerable<SourceLibAm>> GetSources();
        Task<SourceLibAm> UpdateSource(SourceLibAm dataAm);
        Task<SourceLibAm> CreateSource(SourceLibAm dataAm);
        Task CreateSources(List<SourceLibAm> dataAm);
    }
}