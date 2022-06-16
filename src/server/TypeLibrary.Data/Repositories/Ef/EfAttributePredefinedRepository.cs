using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributePredefinedRepository : GenericRepository<TypeLibraryDbContext, AttributePredefinedLibDm>, IEfAttributePredefinedRepository
    {
        public EfAttributePredefinedRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}