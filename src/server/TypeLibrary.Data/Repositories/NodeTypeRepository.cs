using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeRepository : GenericRepository<TypeLibraryDbContext, NodeType>, INodeTypeRepository
    {
        public NodeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
