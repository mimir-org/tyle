using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface ICollectionRepository : IGenericRepository<TypeLibraryDbContext, Collection>
    {
    }
}