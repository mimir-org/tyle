using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfTransportRepository : IGenericRepository<TypeLibraryDbContext, TransportLibDm>
    {
        Task<TransportLibDm> Get(string id);
        IQueryable<TransportLibDm> GetAllTransports();
        IQueryable<TransportLibDm> FindTransport(string id);
    }
}