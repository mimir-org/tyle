using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class SourceRepository : GenericRepository<TypeLibraryDbContext, Source>, ISourceRepository
    {
        public SourceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
