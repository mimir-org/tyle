using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ISimpleRepository
    {
        Task<SimpleLibDm> Get(string id);
        IEnumerable<SimpleLibDm> Get();
        Task Create(SimpleLibDm dataDm);
        void ClearAllChangeTrackers();
    }
}