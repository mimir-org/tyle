using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class TransportTypeRepository : GenericRepository<TypeLibraryDbContext, TransportType>, ITransportTypeRepository
    {
        public TransportTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
