using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class InterfaceService : IInterfaceService
    {
        private readonly IMapper _mapper;
        private readonly IInterfaceRepository _interfaceRepository;
        private readonly IRdsRepository _rdsRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IPurposeRepository _purposeRepository;

        public InterfaceService(IPurposeRepository purposeRepository, IAttributeRepository attributeRepository, IRdsRepository rdsRepository, IMapper mapper, IInterfaceRepository interfaceRepository)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _interfaceRepository = interfaceRepository;
        }

        public async Task<InterfaceLibCm> GetInterface(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get interface. The id is missing value.");

            var data = await _interfaceRepository.FindInterface(id).FirstOrDefaultAsync();

            if (data == null)
                throw new MimirorgNotFoundException($"There is no interface with id: {id}");

            if (data.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var interfaceLibCm = _mapper.Map<InterfaceLibCm>(data);

            if (interfaceLibCm == null)
                throw new MimirorgMappingException("InterfaceLibDm", "InterfaceLibCm");

            return interfaceLibCm;
        }

        public Task<IEnumerable<InterfaceLibCm>> GetInterfaces()
        {
            var interfaces = _interfaceRepository.GetAllInterfaces().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var interfaceLibCms = _mapper.Map<IEnumerable<InterfaceLibCm>>(interfaces);

            if (interfaces.Any() && (interfaceLibCms == null || !interfaceLibCms.Any()))
                throw new MimirorgMappingException("List<InterfaceLibDm>", "ICollection<InterfaceLibAm>");

            return Task.FromResult(interfaceLibCms ?? new List<InterfaceLibCm>());
        }

        public async Task<InterfaceLibCm> CreateInterface(InterfaceLibAm dataAm)
        {
            var existingInterface = await _interfaceRepository.GetAsync(dataAm.Id);

            if (existingInterface != null)
                throw new MimirorgBadRequestException($"There is already registered an interface with name: {existingInterface.Name} with version: {existingInterface.Version}");

            var interfaceLibDm = _mapper.Map<InterfaceLibDm>(dataAm);

            if (interfaceLibDm == null)
                throw new MimirorgMappingException("InterfaceLibAm", "InterfaceLibDm");

            interfaceLibDm.RdsName = _rdsRepository.FindBy(x => x.Id == interfaceLibDm.RdsCode)?.First()?.Name;
            interfaceLibDm.PurposeName = _purposeRepository.FindBy(x => x.Id == interfaceLibDm.PurposeName)?.First()?.Name;

            _attributeRepository.Attach(interfaceLibDm.Attributes, EntityState.Unchanged);

            await _interfaceRepository.CreateAsync(interfaceLibDm);
            await _interfaceRepository.SaveAsync();

            _attributeRepository.Detach(interfaceLibDm.Attributes);
            _interfaceRepository.Detach(interfaceLibDm);

            var createdObject = _mapper.Map<InterfaceLibCm>(interfaceLibDm);

            if (createdObject == null)
                throw new MimirorgMappingException("InterfaceLibDm", "InterfaceLibCm");

            return createdObject;
        }

        public void ClearAllChangeTrackers()
        {
            _interfaceRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}