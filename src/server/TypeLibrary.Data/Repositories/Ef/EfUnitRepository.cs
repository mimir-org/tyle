using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfUnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IEfUnitRepository
    {
        public EfUnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UnitLibDm> Get()
        {
            return GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<UnitLibDm> dataDm)
        {
            foreach (var data in dataDm)
                Attach(data, EntityState.Added);

            await SaveAsync();

            foreach (var data in dataDm)
                Detach(data);
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }

        public void SetUnchanged(ICollection<UnitLibDm> items)
        {
            Attach(items, EntityState.Unchanged);
        }

        public void SetDetached(ICollection<UnitLibDm> items)
        {
            Detach(items);
        }
    }
}