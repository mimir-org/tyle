using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeRepository : GenericRepository<TypeLibraryDbContext, LibraryTypeLibDm>, ILibraryTypeRepository

    {
        public LibraryTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
