using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, IEfTerminalRepository
    {
        private readonly IAttributeRepository _attributeRepository;

        public EfTerminalRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
        }

        public IEnumerable<TerminalLibDm> Get()
        {
            return GetAll().Include(x => x.Attributes).Where(x => !x.Deleted);
        }

        public async Task<TerminalLibDm> Get(string id)
        {
            var terminal = await FindBy(x => x.Id == id)
                .Include(x => x.Attributes)
                .FirstOrDefaultAsync();

            return terminal;
        }

        public async Task Create(List<TerminalLibDm> items)
        {
            foreach (var item in items)
            {
                await Create(item);
            }
        }

        public async Task<TerminalLibDm> Create(TerminalLibDm terminal)
        {
            _attributeRepository.SetUnchanged(terminal.Attributes);
            await CreateAsync(terminal);
            await SaveAsync();
            _attributeRepository.SetDetached(terminal.Attributes);
            return terminal;
        }

        public IEnumerable<TerminalLibDm> GetVersions(string firstVersionId)
        {
            return GetAll()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).Include(x => x.Parent).Include(x => x.Attributes).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();
        }

        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }

        public void SetUnchanged(ICollection<TerminalLibDm> items)
        {
            Attach(items, EntityState.Unchanged);
        }

        public void SetDetached(ICollection<TerminalLibDm> items)
        {
            Detach(items);
        }
    }
}