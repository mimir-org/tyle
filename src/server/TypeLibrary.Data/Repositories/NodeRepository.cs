using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class NodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, INodeRepository
    {
        public NodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
