using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TypeRepository : GenericRepository<TypeLibraryDbContext, TypeLibDm>, ITypeRepository

    {
        public TypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
