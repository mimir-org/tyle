using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, IEfTerminalRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public EfTerminalRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<TerminalLibDm> Get()
        {
            return GetAll();
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

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} not found, delete failed.");

            //if (dm.CreatedBy == _applicationSettings.System)
            //    throw new MimirorgBadRequestException($"The terminal with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;
            Context.Entry(dm).State = EntityState.Modified;
            return await Context.SaveChangesAsync() == 1;
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