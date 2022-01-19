using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class QualifierRepository : GenericRepository<TypeLibraryDbContext, Qualifier>, IQualifierRepository
    {
        public QualifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
