using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceLibDm>, IInterfaceRepository
    {
        public InterfaceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<InterfaceLibDm> GetAllInterfaces()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Purpose)
                .Include(x => x.Parent);
        }

        public IQueryable<InterfaceLibDm> FindInterface(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .ThenInclude(y => y.Units)
                .Include(x => x.Purpose)
                .Include(x => x.Parent);
        }
    }
}
