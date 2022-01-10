using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Data.Contracts
{
    public interface ILibraryTypeRepository : IGenericRepository<TypeLibraryDbContext, LibraryType>
    {

    }
}
