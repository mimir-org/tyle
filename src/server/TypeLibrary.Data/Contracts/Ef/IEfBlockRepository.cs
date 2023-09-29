using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfBlockRepository : IGenericRepository<TypeLibraryDbContext, BlockLibDm>, IBlockRepository
{

}