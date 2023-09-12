using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockTerminalRepository : GenericRepository<TypeLibraryDbContext, BlockTerminalTypeReference>, IEfBlockTerminalRepository
{
    public EfBlockTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}