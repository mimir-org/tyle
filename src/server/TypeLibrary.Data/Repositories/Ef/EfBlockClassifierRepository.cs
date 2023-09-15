using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockClassifierRepository : GenericRepository<TypeLibraryDbContext, BlockClassifierMapping>, IEfBlockClassifierRepository
{
    public EfBlockClassifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}