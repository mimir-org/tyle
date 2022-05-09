using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class NodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, INodeRepository
    {
        public NodeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<NodeLibDm> GetAllNodes()
        {
            return GetAll()
                .Include(x => x.Parent)
                .Include(x => x.NodeTerminals)
                .ThenInclude(y => y.Terminal)
                .ThenInclude(y => y.Parent)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }

        public IQueryable<NodeLibDm> FindNode(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Parent)
                .Include(x => x.NodeTerminals)
                .ThenInclude(y => y.Terminal)
                .ThenInclude(y => y.Parent)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }
    }
}
