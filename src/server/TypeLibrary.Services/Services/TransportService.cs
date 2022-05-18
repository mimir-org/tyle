using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TransportService : ITransportService
    {
        private readonly IMapper _mapper;
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public TransportService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IEfTransportRepository transportRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _transportRepository = transportRepository;
            _mapper = mapper;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<TransportLibCm> GetTransport(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get transport. The id is missing value.");

            var transport = await _transportRepository.FindTransport(id).FirstOrDefaultAsync();

            if (transport == null)
                throw new MimirorgNotFoundException($"There is no transport with id: {id}");

            if (transport.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var transportLibCm = _mapper.Map<TransportLibCm>(transport);

            if (transportLibCm == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return transportLibCm;
        }

        public Task<IEnumerable<TransportLibCm>> GetTransports()
        {
            var transports = _transportRepository.GetAllTransports().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var transportLibCms = _mapper.Map<IEnumerable<TransportLibCm>>(transports);

            if (transports.Any() && (transportLibCms == null || !transportLibCms.Any()))
                throw new MimirorgMappingException("List<TransportLibDm>", "ICollection<TransportLibAm>");

            return Task.FromResult(transportLibCms ?? new List<TransportLibCm>());
        }

        public async Task<TransportLibCm> CreateTransport(TransportLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _transportRepository.GetAsync(dataAm.Id);

            if (existing != null)
            {
                var errorText = $"Transport '{existing.Name}' with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db";

                throw existing.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

            var transportLibDm = _mapper.Map<TransportLibDm>(dataAm);

            if (transportLibDm == null)
                throw new MimirorgMappingException("TransportLibAm", "TransportLibDm");

            transportLibDm.RdsName = _rdsRepository.FindBy(x => x.Id == transportLibDm.RdsCode)?.First()?.Name;
            transportLibDm.PurposeName = _purposeRepository.FindBy(x => x.Id == transportLibDm.PurposeName)?.First()?.Name;

            _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);

            await _transportRepository.CreateAsync(transportLibDm);
            await _transportRepository.SaveAsync();

            _attributeRepository.Detach(transportLibDm.Attributes);
            _transportRepository.Detach(transportLibDm);

            var createdObject = _mapper.Map<TransportLibCm>(transportLibDm);

            if (createdObject == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return createdObject;
        }

        public async Task<IEnumerable<TransportLibCm>> CreateTransports(IEnumerable<TransportLibAm> transports, bool createdBySystem = false)
        {
            var validation = transports.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create transports", validation);

            var existingTransportTypes = await GetTransports();
            var transportsToCreate = transports.Where(x => existingTransportTypes.All(y => y.Id != x.Id)).ToList();
            var transportDmList = _mapper.Map<IEnumerable<TransportLibDm>>(transportsToCreate).ToList();

            if (transportDmList == null || (!transportDmList.Any() && transportsToCreate.Any()))
                throw new MimirorgMappingException("ICollection<TransportLibDm>", "ICollection<TransportLibAm>");

            var allRds = _rdsRepository.GetAll();
            var allPurposes = _purposeRepository.GetAll();

            foreach (var transportLibDm in transportDmList)
            {
                transportLibDm.RdsName = allRds.FirstOrDefault(x => x.Id == transportLibDm.RdsCode)?.Name;
                transportLibDm.PurposeName = allPurposes.FirstOrDefault(x => x.Id == transportLibDm.PurposeName)?.Name;

                _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);

                transportLibDm.CreatedBy = createdBySystem ? _applicationSettings.System : transportLibDm.CreatedBy;

                await _transportRepository.CreateAsync(transportLibDm);
                await _transportRepository.SaveAsync();

                _attributeRepository.Detach(transportLibDm.Attributes);
                _transportRepository.Detach(transportLibDm);
            }

            return _mapper.Map<ICollection<TransportLibCm>>(transportDmList);
        }

        //TODO: This is (temporary) a create/delete method to maintain compatibility with Mimir TypeEditor.
        //TODO: This method need to be rewritten as a proper update method with versioning logic.
        public async Task<TransportLibCm> UpdateTransport(TransportLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update a transport without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update a transport when dataAm is null.");

            if (id == dataAm.Id)
                throw new MimirorgBadRequestException("Not allowed to update: Name, RdsCode or Aspect.");

            var existingDm = await _transportRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgNotFoundException($"Transport with id {id} does not exist.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be updated.");

            var created = await CreateTransport(dataAm);

            if (created?.Id != null)
                throw new MimirorgBadRequestException($"Unable to update transport with id {id}");

            return await DeleteTransport(id) ? created : throw new MimirorgBadRequestException($"Unable to delete transport with id {id}");
        }

        public async Task<bool> DeleteTransport(string id)
        {
            var dm = await _transportRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

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