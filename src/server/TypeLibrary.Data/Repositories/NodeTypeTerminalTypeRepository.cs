using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeTerminalTypeRepository : GenericRepository<ModelBuilderDbContext, NodeTypeTerminalType>, INodeTypeTerminalTypeRepository
    {
        public NodeTypeTerminalTypeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
