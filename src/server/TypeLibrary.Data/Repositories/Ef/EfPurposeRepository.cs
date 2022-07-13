using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfPurposeRepository : GenericRepository<TypeLibraryDbContext, PurposeLibDm>, IEfPurposeRepository
    {
        public EfPurposeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<PurposeLibDm> Get()
        {
            return GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<PurposeLibDm> dataDm)
        {
            foreach (var data in dataDm)
                Attach(data, EntityState.Added);

            await SaveAsync();

            foreach (var data in dataDm)
                Detach(data);
        }

        public void SetAdded(ICollection<PurposeLibDm> items)
        {
            Attach(items, EntityState.Added);
        }

        public void SetDetached(ICollection<PurposeLibDm> items)
        {
            Detach(items);
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}