using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfRdsRepository : GenericRepository<TypeLibraryDbContext, RdsLibDm>, IEfRdsRepository
    {
        public EfRdsRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
