using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeSourceRepository : GenericRepository<TypeLibraryDbContext, AttributeSourceLibDm>, ISourceRepository
    {
        public AttributeSourceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
