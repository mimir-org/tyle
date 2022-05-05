using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IInterfaceService
    {
        Task<InterfaceLibCm> GetInterface(string id);
        Task<IEnumerable<InterfaceLibCm>> GetInterfaces();
        Task<InterfaceLibCm> CreateInterface(InterfaceLibAm dataAm);
        void ClearAllChangeTrackers();
    }
}