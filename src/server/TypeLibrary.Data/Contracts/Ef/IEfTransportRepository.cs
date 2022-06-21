using System.Linq;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfTransportRepository : IGenericRepository<TypeLibraryDbContext, TransportLibDm>
    {
        IQueryable<TransportLibDm> GetAllTransports();
        IQueryable<TransportLibDm> FindTransport(string id);
    }
}