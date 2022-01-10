using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeRepository : GenericRepository<TypeLibraryDbContext, LibraryType>, ILibraryTypeRepository

    {
        public LibraryTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
