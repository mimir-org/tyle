using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IInterfaceService
    {
        Task<InterfaceLibCm> Get(string id);
        Task<IEnumerable<InterfaceLibCm>> GetLatestVersions();
        Task<InterfaceLibCm> Create(InterfaceLibAm dataAm, bool resetVersion);
        Task<InterfaceLibCm> Update(InterfaceLibAm dataAm, string id);
        Task<InterfaceLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string interfaceId, int companyId);
    }
}