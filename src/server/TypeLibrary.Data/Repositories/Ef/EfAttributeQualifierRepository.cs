using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeQualifierRepository : GenericRepository<TypeLibraryDbContext, AttributeQualifierLibDm>, IEfAttributeQualifierRepository
    {
        public EfAttributeQualifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}