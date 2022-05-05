using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITransportService
    {
        Task<TransportLibCm> GetTransport(string id);
        Task<IEnumerable<TransportLibCm>> GetTransports();
        Task<TransportLibCm> CreateTransport(TransportLibAm dataAm);
        Task<IEnumerable<TransportLibCm>> CreateTransports(IEnumerable<TransportLibAm> dataAms);
        void ClearAllChangeTrackers();
    }
}