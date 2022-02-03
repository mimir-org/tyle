using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class SourceRepository : GenericRepository<TypeLibraryDbContext, SourceLibDm>, ISourceRepository
    {
        public SourceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
