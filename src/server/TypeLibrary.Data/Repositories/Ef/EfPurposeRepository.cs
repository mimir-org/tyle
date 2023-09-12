using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfPurposeRepository : GenericRepository<TypeLibraryDbContext, PurposeReference>, IEfPurposeRepository
{
    public EfPurposeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}