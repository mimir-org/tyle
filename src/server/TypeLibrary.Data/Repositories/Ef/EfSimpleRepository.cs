using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfSimpleRepository : GenericRepository<TypeLibraryDbContext, SimpleLibDm>, IEfSimpleRepository
    {
        public EfSimpleRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<SimpleLibDm> GetAllSimple()
        {
            return GetAll().Include(x => x.Attributes);

        }

        public IQueryable<SimpleLibDm> FindSimple(string id)
        {
            return FindBy(x => x.Id == id).Include(x => x.Attributes);
        }
    }
}
