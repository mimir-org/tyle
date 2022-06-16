using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IInterfaceService
    {
        Task<InterfaceLibCm> Get(string id);
        Task<IEnumerable<InterfaceLibCm>> GetLatestVersions();
        Task<InterfaceLibCm> Create(InterfaceLibAm dataAm);
        Task<InterfaceLibCm> Update(InterfaceLibAm dataAm, string id);
        Task<bool> Delete(string id);
    }
}