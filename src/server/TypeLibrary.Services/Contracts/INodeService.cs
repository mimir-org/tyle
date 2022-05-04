using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface INodeService
    {
        Task<NodeLibCm> GetNode(string id);
        Task<IEnumerable<NodeLibCm>> GetNodes();
        Task<NodeLibCm> UpdateNode(NodeLibAm dataAm, string id);
        Task<NodeLibCm> CreateNode(NodeLibAm dataAm);
        Task<bool> DeleteNode(string id);
        void ClearAllChangeTrackers();
    }
}