using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeTerminalTypeRepository : GenericRepository<TypeLibraryDbContext, NodeTerminalDm>, INodeTypeTerminalTypeRepository
    {
        public NodeTypeTerminalTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
