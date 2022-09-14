using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITransportRepository
    {
        IEnumerable<TransportLibDm> Get();
        Task<TransportLibDm> Get(string id);
        Task UpdateState(string id, State state);
        Task Create(TransportLibDm dataDm, State state);
        Task<bool> Remove(string id);
        void ClearAllChangeTrackers();
    }
}