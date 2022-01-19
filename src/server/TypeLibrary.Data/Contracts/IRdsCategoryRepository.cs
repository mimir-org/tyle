using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsCategoryRepository : IGenericRepository<TypeLibraryDbContext, RdsCategory>
    {
    }
}