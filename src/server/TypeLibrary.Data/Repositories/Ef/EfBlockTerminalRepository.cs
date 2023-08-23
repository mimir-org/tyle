using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfBlockTerminalRepository : GenericRepository<TypeLibraryDbContext, BlockTerminalLibDm>, IEfBlockTerminalRepository
{
    public EfBlockTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}