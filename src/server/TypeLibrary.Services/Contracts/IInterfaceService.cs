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
        Task<InterfaceLibCm> UpdateInterface(InterfaceLibAm dataAm, string id);
        Task<bool> DeleteInterface(string id);
        void ClearAllChangeTrackers();
    }
}