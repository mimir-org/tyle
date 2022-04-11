using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleRepository : GenericRepository<TypeLibraryDbContext, SimpleLibDm>, ISimpleRepository
    {
        public SimpleRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<SimpleLibDm> GetAllSimples()
        {
            return GetAll().Include(x => x.Attributes);

        }

        public IQueryable<SimpleLibDm> FindSimple(string id)
        {
            return FindBy(x => x.Id == id).Include(x => x.Attributes);
        }
    }
}
