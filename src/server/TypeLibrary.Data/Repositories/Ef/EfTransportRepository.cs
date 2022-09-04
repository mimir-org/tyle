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
    public class EfTransportRepository : GenericRepository<TypeLibraryDbContext, TransportLibDm>, IEfTransportRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IRdsRepository _rdsRepository;
        private readonly IPurposeRepository _purposeRepository;

        public EfTransportRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, IRdsRepository rdsRepository, IPurposeRepository purposeRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _purposeRepository = purposeRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<TransportLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .Where(x => !x.Deleted)
                .OrderBy(x => x.Name)
                .AsSplitQuery();
        }

        public async Task<TransportLibDm> Get(string id)
        {
            var item = await FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task Create(TransportLibDm dataDm)
        {
            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetUnchanged(dataDm.Attributes);

            await CreateAsync(dataDm);
            await SaveAsync();

            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetDetached(dataDm.Attributes);

            Detach(dataDm);
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Transport with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;
            Context.Entry(dm).State = EntityState.Modified;
            return await Context.SaveChangesAsync() == 1;
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
            _rdsRepository.ClearAllChangeTrackers();
            _attributeRepository.ClearAllChangeTrackers();
            _purposeRepository.ClearAllChangeTrackers();
        }
    }
}