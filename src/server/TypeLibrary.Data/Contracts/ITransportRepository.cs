using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITransportRepository
    {
        IEnumerable<TransportLibDm> Get();
        Task<TransportLibDm> Get(string id);
        Task Create(TransportLibDm dataDm);
        Task<bool> Delete(string id);
        void ClearAllChangeTrackers();
    }
}