using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfMediumRepository : GenericRepository<TypeLibraryDbContext, MediumReference>, IEfMediumRepository
{
    public EfMediumRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}