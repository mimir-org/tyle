using System.Collections.Generic;
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
    public class EfInterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceLibDm>, IEfInterfaceRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public EfInterfaceRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<InterfaceLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .OrderBy(x => x.Name)
                .AsSplitQuery();
        }

        public async Task<InterfaceLibDm> Get(string id)
        {
            var item = await FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .ThenInclude(y => y.Units)
                .Include(x => x.Parent)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => !x.Deleted);

            return item;
        }

        public async Task Create(InterfaceLibDm dataDm)
        {
            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetUnchanged(dataDm.Attributes);

            await CreateAsync(dataDm);
            await SaveAsync();

            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetDetached(dataDm.Attributes);

            Detach(dataDm);

            ClearAllChangeTrackers();
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;
            Update(dm);

            var status = await SaveAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }
    }
}