using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeRepository : GenericRepository<TypeLibraryDbContext, LibraryType>, ILibraryTypeRepository

    {
        public LibraryTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
