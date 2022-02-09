using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeFormatRepository : GenericRepository<TypeLibraryDbContext, AttributeFormatLibDm>, IFormatRepository
    {
        public AttributeFormatRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
