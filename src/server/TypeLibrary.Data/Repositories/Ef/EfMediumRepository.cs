using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfMediumRepository : GenericRepository<TypeLibraryDbContext, MediumReference>, IEfMediumRepository
{
    public EfMediumRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}