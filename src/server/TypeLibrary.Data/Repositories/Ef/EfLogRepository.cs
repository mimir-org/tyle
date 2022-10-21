using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfLogRepository : GenericRepository<TypeLibraryDbContext, LogLibDm>, IEfLogRepository
    {
        public EfLogRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <returns>A collection of logs</returns>
        public IEnumerable<LogLibDm> Get()
        {
            return GetAll();
        }

        /// <summary>
        /// Create a log entry
        /// </summary>
        /// <param name="logDms">The log entry to be created</param>
        public async Task Create(ICollection<LogLibDm> logDms)
        {
            foreach (var dm in logDms)
            {
                await CreateAsync(dm);
            }

            await SaveAsync();
        }
    }
}