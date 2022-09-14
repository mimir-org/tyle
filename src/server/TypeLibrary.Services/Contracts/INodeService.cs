using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface INodeService
    {
        Task<NodeLibCm> Get(string id);
        Task<IEnumerable<NodeLibCm>> GetAll(bool includeDeleted = false);
        Task<IEnumerable<NodeLibCm>> GetLatestVersions();
        Task<NodeLibCm> Create(NodeLibAm dataAm, bool resetVersion = false);
        Task<NodeLibCm> Update(NodeLibAm dataAm, string id);
        Task<NodeLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string nodeId, int companyId);
    }
}