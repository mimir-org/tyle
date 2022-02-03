using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TransportRepository : GenericRepository<TypeLibraryDbContext, TransportLibDm>, ITransportRepository
    {
        public TransportRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
