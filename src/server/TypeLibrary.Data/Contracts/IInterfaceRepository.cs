using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IInterfaceRepository
    {
        IEnumerable<InterfaceLibDm> Get();
        Task<InterfaceLibDm> Get(string id);
        Task Create(InterfaceLibDm dataDm);
        Task<bool> Delete(string id);
        void ClearAllChangeTrackers();
    }
}