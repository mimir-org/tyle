using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeAspectRepository : GenericRepository<TypeLibraryDbContext, AttributeAspectLibDm>, IEfAttributeAspectRepository
    {
        public EfAttributeAspectRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
