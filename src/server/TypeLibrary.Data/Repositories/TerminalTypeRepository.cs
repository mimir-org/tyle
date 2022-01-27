using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TerminalTypeRepository : GenericRepository<TypeLibraryDbContext, TerminalDm>, ITerminalTypeRepository
    {
        public TerminalTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
