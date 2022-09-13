using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfSimpleRepository : GenericRepository<TypeLibraryDbContext, SimpleLibDm>, IEfSimpleRepository
    {
        private readonly IAttributeRepository _attributeRepository;

        public EfSimpleRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
        }

        public Task<SimpleLibDm> Get(string id)
        {
            return FindBy(x => x.Id == id).Include(x => x.Attributes).FirstOrDefaultAsync();
        }

        public IEnumerable<SimpleLibDm> Get()
        {
            return GetAll().Include(x => x.Attributes);
        }

        public async Task Create(SimpleLibDm simple, State state)
        {
            if (simple.Attributes != null && simple.Attributes.Any())
                _attributeRepository.SetUnchanged(simple.Attributes);

            simple.State = state;

            await CreateAsync(simple);
            await SaveAsync();

            if (simple.Attributes != null && simple.Attributes.Any())
                _attributeRepository.SetDetached(simple.Attributes);

            Detach(simple);
        }

        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }

        public void SetUnchanged(ICollection<SimpleLibDm> items)
        {
            Attach(items, EntityState.Unchanged);
        }

        public void SetDetached(ICollection<SimpleLibDm> items)
        {
            Detach(items);
        }
    }
}