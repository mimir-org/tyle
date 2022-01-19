using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class FormatRepository : GenericRepository<TypeLibraryDbContext, Format>, IFormatRepository
    {
        public FormatRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
