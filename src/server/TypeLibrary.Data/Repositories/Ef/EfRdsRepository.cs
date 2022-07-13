using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfRdsRepository : GenericRepository<TypeLibraryDbContext, RdsLibDm>, IEfRdsRepository
    {
        public EfRdsRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RdsLibDm> Get()
        {
            return GetAll().Where(x => !x.Deleted);
        }

        public async Task<RdsLibDm> Create(RdsLibDm rds)
        {
            await CreateAsync(rds);
            await SaveAsync();
            Detach(rds);
            return rds;
        }

        public async Task Create(List<RdsLibDm> items)
        {
            Attach(items, EntityState.Added);
            await SaveAsync();
            Detach(items);
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}