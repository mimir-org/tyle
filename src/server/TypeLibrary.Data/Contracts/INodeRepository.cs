using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface INodeRepository : IGenericRepository<TypeLibraryDbContext, NodeLibDm>
    {
        IQueryable<NodeLibDm> GetAllNodes();
        IQueryable<NodeLibDm> FindNode(string id);
    }
}
