using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TerminalTypeRepository : GenericRepository<TypeLibraryDbContext, TerminalType>, ITerminalTypeRepository
    {
        public TerminalTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
