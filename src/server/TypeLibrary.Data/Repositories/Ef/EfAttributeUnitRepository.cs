using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeUnitRepository : GenericRepository<TypeLibraryDbContext, AttributeUnitLibDm>, IEfAttributeUnitRepository
{
    public EfAttributeUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}