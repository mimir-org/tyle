using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeSourceRepository : GenericRepository<TypeLibraryDbContext, AttributeSourceLibDm>, IEfAttributeSourceRepository
    {
        public EfAttributeSourceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}