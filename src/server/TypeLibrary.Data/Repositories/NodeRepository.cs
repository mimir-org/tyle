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
                .Include(x => x.Rds)
                .Include(x => x.Purpose)
                .Include(x => x.Parent)
                .Include(x => x.Blob)
                .Include(x => x.AttributeAspect)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }

        public IQueryable<NodeLibDm> FindNode(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Rds)
                .Include(x => x.Purpose)
                .Include(x => x.Parent)
                .Include(x => x.Blob)
                .Include(x => x.AttributeAspect)
                .Include(x => x.NodeTerminals)
                .Include(x => x.Attributes)
                .Include(x => x.Simples);
        }
    }
}
