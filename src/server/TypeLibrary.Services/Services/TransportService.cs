using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TransportService : ITransportService
    {
        private readonly IMapper _mapper;
        private readonly ITransportRepository _transportRepository;
        private readonly IRdsRepository _rdsRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IPurposeRepository _purposeRepository;

        public TransportService(IPurposeRepository purposeRepository, IAttributeRepository attributeRepository, IRdsRepository rdsRepository, ITransportRepository transportRepository, IMapper mapper)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _transportRepository = transportRepository;
            _mapper = mapper;
        }

        public async Task<TransportLibCm> GetTransport(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get transport. The id is missing value.");

            var transport = await _transportRepository.FindTransport(id).FirstOrDefaultAsync();

            if (transport == null)
                throw new MimirorgNotFoundException($"There is no transport with id: {id}");

            var transportLibCm = _mapper.Map<TransportLibCm>(transport);

            if (transportLibCm == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return transportLibCm;
        }

        public Task<IEnumerable<TransportLibCm>> GetTransports()
        {
            var transports = _transportRepository.GetAllTransports().ToList().OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            var transportLibCms = _mapper.Map<IEnumerable<TransportLibCm>>(transports);

            if (transports.Any() && (transportLibCms == null || !transportLibCms.Any()))
                throw new MimirorgMappingException("List<TransportLibDm>", "ICollection<TransportLibAm>");

            return Task.FromResult(transportLibCms ?? new List<TransportLibCm>());
        }

        public async Task<TransportLibCm> CreateTransport(TransportLibAm transport)
        {
            var existingTransport = await _transportRepository.GetAsync(transport.Id);

            if (existingTransport != null)
                throw new MimirorgBadRequestException($"There is already registered a transport with name: {transport.Name} with version: {transport.Version}");

            var transportLibDm = _mapper.Map<TransportLibDm>(transport);

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

        public async Task<IEnumerable<TransportLibCm>> CreateTransports(IEnumerable<TransportLibAm> transports)
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
                await _transportRepository.CreateAsync(transportLibDm);
                await _transportRepository.SaveAsync();
                _attributeRepository.Detach(transportLibDm.Attributes);
                _transportRepository.Detach(transportLibDm);
            }

            return _mapper.Map<ICollection<TransportLibCm>>(transportDmList);
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