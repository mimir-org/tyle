using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsRepository : IGenericRepository<TypeLibraryDbContext, RdsLibDm>
    {
    }
}
