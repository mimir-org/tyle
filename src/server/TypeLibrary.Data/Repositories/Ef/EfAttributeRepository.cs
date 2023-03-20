using System.Collections.Generic;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
{
    public EfAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<AttributeLibDm> Get()
    {
        return GetAll();
    }
}