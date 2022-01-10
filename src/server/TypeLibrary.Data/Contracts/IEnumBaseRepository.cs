using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Data.Contracts
{
    public interface IEnumBaseRepository : IGenericRepository<TypeLibraryDbContext, EnumBase>
    {
        
    }
}
