using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class TerminalTypeRepository : GenericRepository<TypeLibraryDbContext, TerminalType>, ITerminalTypeRepository
    {
        public TerminalTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
