using System.Collections.Generic;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IEfUnitRepository
{
    public EfUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<UnitLibDm> Get()
    {
        return GetAll();
    }
}