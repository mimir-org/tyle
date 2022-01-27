using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class FormatRepository : GenericRepository<TypeLibraryDbContext, FormatDm>, IFormatRepository
    {
        public FormatRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
