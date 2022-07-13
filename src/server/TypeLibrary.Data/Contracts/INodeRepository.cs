using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeRepository
    {
        IEnumerable<NodeLibDm> Get();
        Task<NodeLibDm> Get(string id);
        Task<NodeLibDm> Create(NodeLibDm node);
        Task<bool> Remove(string id);
        void ClearAllChangeTrackers();
    }
}