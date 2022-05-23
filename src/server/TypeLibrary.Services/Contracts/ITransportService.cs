using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITransportService
    {
        Task<TransportLibCm> Get(string id);
        Task<IEnumerable<TransportLibCm>> GetLatestVersions();
        Task<TransportLibCm> Create(TransportLibAm dataAm);
        Task<IEnumerable<TransportLibCm>> Create(IEnumerable<TransportLibAm> dataAms, bool createdBySystem = false);
        Task<TransportLibCm> Update(TransportLibAm dataAm, string id);
        Task<bool> Delete(string id);
        void ClearAllChangeTrackers();
    }
}