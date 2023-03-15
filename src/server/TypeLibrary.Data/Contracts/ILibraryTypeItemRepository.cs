using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Contracts
{
    public interface ILibraryTypeItemRepository
    {
        Task<IEnumerable<NodeLibCm>> GetNodes(string searchString = null);
        Task<T> GetLibraryItem<T>(int id) where T : class, new();
        void ClearAllChangeTracker();
    }
}