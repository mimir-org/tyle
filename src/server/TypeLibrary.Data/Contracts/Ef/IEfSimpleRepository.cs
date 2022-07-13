using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfSimpleRepository : IGenericRepository<TypeLibraryDbContext, SimpleLibDm>, ISimpleRepository
    {
        
    }
}