using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class EnumBaseRepository: GenericRepository<TypeLibraryDbContext, EnumBase>, IEnumBaseRepository
    {
        public EnumBaseRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
