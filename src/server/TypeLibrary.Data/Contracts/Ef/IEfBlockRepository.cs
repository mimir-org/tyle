using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfBlockRepository : IGenericRepository<TypeLibraryDbContext, BlockType>, IBlockRepository
{

}