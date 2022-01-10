using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsRepository : IGenericRepository<TypeLibraryDbContext, Rds>
    {
    }
}
