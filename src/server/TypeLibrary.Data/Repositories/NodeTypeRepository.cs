using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeRepository : GenericRepository<TypeLibraryDbContext, NodeType>, INodeTypeRepository
    {
        public NodeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
