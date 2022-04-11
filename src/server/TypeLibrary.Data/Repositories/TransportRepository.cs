using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class TransportRepository : GenericRepository<TypeLibraryDbContext, TransportLibDm>, ITransportRepository
    {
        public TransportRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TransportLibDm> GetAllTransports()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Rds)
                .Include(x => x.Purpose)
                .Include(x => x.Parent);
        }

        public IQueryable<TransportLibDm> FindTransport(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .ThenInclude(y => y.Units)
                .Include(x => x.Rds)
                .ThenInclude(y => y.RdsCategory)
                .Include(x => x.Purpose)
                .Include(x => x.Parent);
        }
    }
}
