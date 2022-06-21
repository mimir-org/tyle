using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfNodeRepository : IGenericRepository<TypeLibraryDbContext, NodeLibDm>
    {
        IQueryable<NodeLibDm> GetAllNodes();
        IQueryable<NodeLibDm> FindNode(string id);
    }
}