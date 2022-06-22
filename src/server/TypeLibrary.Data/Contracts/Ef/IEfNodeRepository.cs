using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfNodeRepository : IGenericRepository<TypeLibraryDbContext, NodeLibDm>
    {
        Task<NodeLibDm> Get(string id);
        IQueryable<NodeLibDm> GetAllNodes();
        IQueryable<NodeLibDm> FindNode(string id);
    }
}