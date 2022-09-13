using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeRepository
    {
        IEnumerable<NodeLibDm> Get();
        Task<NodeLibDm> Get(string id);
        Task<NodeLibDm> Create(NodeLibDm node, State state);
        Task<bool> Remove(string id);
        void ClearAllChangeTrackers();
    }
}