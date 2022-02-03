using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionAm>> GetCollections();
        Task<CollectionAm> UpdateCollection(CollectionAm dataAm);
        Task<CollectionAm> CreateCollection(CollectionAm dataAm);
        Task CreateCollections(List<CollectionAm> dataAm);
    }
}