using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ISimpleRepository
    {
        Task<SimpleLibDm> Get(string id);
        IEnumerable<SimpleLibDm> Get();
        Task Create(SimpleLibDm simple);
        void ClearAllChangeTrackers();
        void SetUnchanged(ICollection<SimpleLibDm> items);
        void SetDetached(ICollection<SimpleLibDm> items);
    }
}