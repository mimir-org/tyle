using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfClassifierRepository : GenericRepository<TypeLibraryDbContext, ClassifierReference>, IEfClassifierRepository
{
    public EfClassifierRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}