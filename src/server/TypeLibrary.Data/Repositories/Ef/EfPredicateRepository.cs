using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfPredicateRepository : GenericRepository<TypeLibraryDbContext, PredicateReference>, IEfPredicateRepository
{
    public EfPredicateRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}