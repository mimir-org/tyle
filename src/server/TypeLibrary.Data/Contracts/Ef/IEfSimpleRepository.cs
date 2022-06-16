using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfSimpleRepository : IGenericRepository<TypeLibraryDbContext, SimpleLibDm>
    {
        IQueryable<SimpleLibDm> GetAllSimple();
        IQueryable<SimpleLibDm> FindSimple(string id);
    }
}