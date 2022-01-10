using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeTerminalTypeRepository : GenericRepository<TypeLibraryDbContext, NodeTypeTerminalType>, INodeTypeTerminalTypeRepository
    {
        public NodeTypeTerminalTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
