using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;


namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfAttributeGroupRepository : IGenericRepository<TypeLibraryDbContext, AttributeGroupLibDm>, IAttributeGroupRepository
    {

    }
}
