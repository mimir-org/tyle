using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Client;

namespace TypeLibrary.Data.Contracts
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<NodeCm>> GetNodeTypes(string searchString = null);
        Task<IEnumerable<InterfaceCm>> GetInterfaceTypes(string searchString = null);
        Task<IEnumerable<TransportCm>> GetTransportTypes(string searchString = null);
        Task<T> GetLibraryItem<T>(string id) where T : class, new();
        void ClearAllChangeTracker();
    }
}
