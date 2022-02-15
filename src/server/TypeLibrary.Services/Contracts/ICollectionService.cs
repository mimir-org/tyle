using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionLibCm>> GetCollections();
        Task<CollectionLibCm> UpdateCollection(CollectionLibAm dataAm);
        Task<CollectionLibCm> CreateCollection(CollectionLibAm dataAm);
        Task CreateCollections(List<CollectionLibAm> dataAm);
    }
}