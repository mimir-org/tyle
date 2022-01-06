using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeRepository : GenericRepository<ModelBuilderDbContext, LibraryType>, ILibraryTypeRepository

    {
        public LibraryTypeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
