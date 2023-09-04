using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfAttributeRepository : IGenericRepository<TypeLibraryDbContext, AttributeType>, IAttributeRepository
{

}