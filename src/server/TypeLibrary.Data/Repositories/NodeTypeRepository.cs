using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class NodeTypeRepository : GenericRepository<TypeLibraryDbContext, NodeDm>, INodeTypeRepository
    {
        public NodeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
