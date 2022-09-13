using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Enums;
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
            return GetAll();
        }

        public async Task<RdsLibDm> Create(RdsLibDm rds, State state)
        {
            rds.State = state;
            await CreateAsync(rds);
            await SaveAsync();
            Detach(rds);
            return rds;
        }

        public async Task Create(List<RdsLibDm> items, State state)
        {
            foreach (var item in items)
                item.State = state;
            
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