using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfClassifierRepository : GenericRepository<TypeLibraryDbContext, ClassifierReference>, IEfClassifierRepository
{
    public EfClassifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}