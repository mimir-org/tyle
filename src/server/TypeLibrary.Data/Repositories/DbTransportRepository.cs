using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbTransportRepository : ITransportRepository
    {
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public DbTransportRepository(IOptions<ApplicationSettings> applicationSettings, IEfTransportRepository transportRepository, IEfRdsRepository rdsRepository, IEfAttributeRepository attributeRepository, IEfPurposeRepository purposeRepository)
        {
            _transportRepository = transportRepository;
            _rdsRepository = rdsRepository;
            _attributeRepository = attributeRepository;
            _purposeRepository = purposeRepository;
            _applicationSettings = applicationSettings?.Value;
        }


        public IEnumerable<TransportLibDm> Get()
        {
            return _transportRepository.GetAllTransports().Where(x => !x.Deleted);
        }

        public async Task<TransportLibDm> Get(string id)
        {
            return await _transportRepository.FindTransport(id).FirstOrDefaultAsync(x => !x.Deleted);
        }

        public async Task Create(TransportLibDm dataDm)
        {
            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.Attach(dataDm.Attributes, EntityState.Unchanged);

            await _transportRepository.CreateAsync(dataDm);
            await _transportRepository.SaveAsync();

            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.Detach(dataDm.Attributes);

            _transportRepository.Detach(dataDm);
        }

        public async Task<bool> Delete(string id)
        {
            var dm = await _transportRepository.Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Transport with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            _transportRepository.Update(dm);
            var status = await _transportRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _transportRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}