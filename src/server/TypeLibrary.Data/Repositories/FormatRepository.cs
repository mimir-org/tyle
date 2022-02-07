using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class FormatRepository : GenericRepository<TypeLibraryDbContext, FormatLibDm>, IFormatRepository
    {
        public FormatRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
