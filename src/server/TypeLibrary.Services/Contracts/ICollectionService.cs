using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionLibAm>> GetCollections();
        Task<CollectionLibAm> UpdateCollection(CollectionLibAm dataAm);
        Task<CollectionLibAm> CreateCollection(CollectionLibAm dataAm);
        Task CreateCollections(List<CollectionLibAm> dataAm);
    }
}