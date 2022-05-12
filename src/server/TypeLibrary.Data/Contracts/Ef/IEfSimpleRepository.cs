using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEFSimpleRepository : IGenericRepository<TypeLibraryDbContext, SimpleLibDm>
    {
        IQueryable<SimpleLibDm> GetAllSimples();
        IQueryable<SimpleLibDm> FindSimple(string id);
    }
}
