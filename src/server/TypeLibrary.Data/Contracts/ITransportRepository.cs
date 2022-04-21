using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITransportRepository : IGenericRepository<TypeLibraryDbContext, TransportLibDm>
    {
        IQueryable<TransportLibDm> GetAllTransports();
        IQueryable<TransportLibDm> FindTransport(string id);
    }
}
