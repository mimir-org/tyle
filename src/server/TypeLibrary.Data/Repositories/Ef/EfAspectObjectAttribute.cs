using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAspectObjectAttributeRepository : GenericRepository<TypeLibraryDbContext, AspectObjectAttributeLibDm>, IEfAspectObjectAttributeRepository
{
    public EfAspectObjectAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}