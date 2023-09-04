using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockAttributeRepository : GenericRepository<TypeLibraryDbContext, BlockAttributeTypeReference>, IEfBlockAttributeRepository
{
    public EfBlockAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}