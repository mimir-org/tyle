using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTerminalRepository : GenericRepository<TypeLibraryDbContext, NodeTerminalLibDm>, INodeTerminalRepository
    {
        public NodeTerminalRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
