using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockAttributeRepository : GenericRepository<TypeLibraryDbContext, BlockAttributeTypeReference>, IEfBlockAttributeRepository
{
    public EfBlockAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}