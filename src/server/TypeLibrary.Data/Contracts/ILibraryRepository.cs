using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Client;

namespace TypeLibrary.Data.Contracts
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<NodeCm>> GetNodes(string searchString = null);
        Task<IEnumerable<InterfaceCm>> GetInterfaces(string searchString = null);
        Task<IEnumerable<TransportCm>> GetTransports(string searchString = null);
        Task<T> GetLibraryItem<T>(string id) where T : class, new();
        void ClearAllChangeTracker();
    }
}
