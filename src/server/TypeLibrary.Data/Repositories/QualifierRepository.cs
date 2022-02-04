using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class QualifierRepository : GenericRepository<TypeLibraryDbContext, QualifierLibDm>, IQualifierRepository
    {
        public QualifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
