using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
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
