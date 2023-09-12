using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfSymbolRepository : IGenericRepository<TypeLibraryDbContext, SymbolLibDm>, IDynamicSymbolDataProvider, ISymbolRepository
{
}