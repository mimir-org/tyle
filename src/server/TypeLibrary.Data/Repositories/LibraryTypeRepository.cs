using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeRepository : GenericRepository<TypeLibraryDbContext, LibraryTypeLibDm>, ILibraryTypeRepository

    {
        public LibraryTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
