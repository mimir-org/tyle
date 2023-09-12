using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitReference>, IEfUnitRepository
{
    public EfUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}