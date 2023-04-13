using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfAspectObjectTerminalRepository : GenericRepository<TypeLibraryDbContext, AspectObjectTerminalLibDm>, IEfAspectObjectTerminalRepository
{
    public EfAspectObjectTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}