using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsCategoryRepository : IGenericRepository<TypeLibraryDbContext, RdsCategoryLibDm>
    {
    }
}