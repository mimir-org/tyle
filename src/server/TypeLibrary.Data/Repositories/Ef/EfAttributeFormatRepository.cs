using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeFormatRepository : GenericRepository<TypeLibraryDbContext, AttributeFormatLibDm>, IEfAttributeFormatRepository
    {
        public EfAttributeFormatRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}