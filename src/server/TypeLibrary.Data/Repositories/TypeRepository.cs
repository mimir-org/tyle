using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TypeRepository : GenericRepository<TypeLibraryDbContext, TypeDm>, ITypeRepository

    {
        public TypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
