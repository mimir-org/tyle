using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfSymbolRepository : IGenericRepository<TypeLibraryDbContext, SymbolLibDm>, IDynamicImageDataProvider
    {
    }
}
