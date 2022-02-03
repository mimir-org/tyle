using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Client;

namespace TypeLibrary.Data.Contracts
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<NodeLibCm>> GetNodes(string searchString = null);
        Task<IEnumerable<InterfaceLibCm>> GetInterfaces(string searchString = null);
        Task<IEnumerable<TransportLibCm>> GetTransports(string searchString = null);
        Task<T> GetLibraryItem<T>(string id) where T : class, new();
        void ClearAllChangeTracker();
    }
}
