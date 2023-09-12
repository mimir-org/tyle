using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeUnitRepository : GenericRepository<TypeLibraryDbContext, AttributeUnitMapping>, IEfAttributeUnitRepository
{
    public EfAttributeUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}