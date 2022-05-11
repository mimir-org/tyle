using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ISimpleService
    {
        Task<SimpleLibCm> GetSimple(string id);
        Task<IEnumerable<SimpleLibCm>> GetAllSimple();
        Task<SimpleLibCm> CreateSimple(SimpleLibAm dataAm);
        Task<IEnumerable<SimpleLibCm>> CreateSimple(IEnumerable<SimpleLibAm> dataAms, bool createdBySystem = false);
        void ClearAllChangeTrackers();
    }
}