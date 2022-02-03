using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, ITerminalRepository
    {
        public TerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
