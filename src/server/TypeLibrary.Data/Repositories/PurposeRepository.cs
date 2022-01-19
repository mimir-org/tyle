using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class PurposeRepository : GenericRepository<TypeLibraryDbContext, Purpose>, IPurposeRepository
    {
        public PurposeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
