using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TerminalNodeRepository : GenericRepository<TypeLibraryDbContext, TerminalNodeLibDm>, ITerminalNodeRepository
    {
        public TerminalNodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
