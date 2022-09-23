using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IInterfaceRepository
    {
        Task<int> ChangeParentId(string oldId, string newId);
        IEnumerable<InterfaceLibDm> Get();
        Task<InterfaceLibDm> Get(string id);
        Task UpdateState(string id, State state);
        Task Create(InterfaceLibDm dataDm, State state);
        Task<bool> Remove(string id);
        void ClearAllChangeTrackers();
    }
}