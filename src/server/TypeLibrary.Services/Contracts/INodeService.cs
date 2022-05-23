using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface INodeService
    {
        Task<NodeLibCm> Get(string id);
        Task<IEnumerable<NodeLibCm>> GetLatestVersions();
        Task<NodeLibCm> Create(NodeLibAm dataAm);
        Task<NodeLibCm> Update(NodeLibAm dataAm, string id);
        Task<bool> Delete(string id);
        void ClearAllChangeTrackers();
    }
}