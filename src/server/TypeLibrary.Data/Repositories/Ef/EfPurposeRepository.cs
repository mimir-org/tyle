using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfPurposeRepository : GenericRepository<TypeLibraryDbContext, PurposeReference>, IEfPurposeRepository
{
    public EfPurposeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}