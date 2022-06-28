using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfTransportRepository : GenericRepository<TypeLibraryDbContext, TransportLibDm>, IEfTransportRepository
    {
        public EfTransportRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TransportLibDm> Get(string id)
        {
            return await GetAsync(id);
        }

        public IQueryable<TransportLibDm> GetAllTransports()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent);
        }

        public IQueryable<TransportLibDm> FindTransport(string id)
        {
            return FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .ThenInclude(y => y.Units)
                .Include(x => x.Parent);
        }
    }
}