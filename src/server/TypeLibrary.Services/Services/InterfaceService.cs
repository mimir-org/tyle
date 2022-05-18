using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class InterfaceService : IInterfaceService
    {
        private readonly IMapper _mapper;
        private readonly IEfInterfaceRepository _interfaceRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public InterfaceService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IMapper mapper, IEfInterfaceRepository interfaceRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _interfaceRepository = interfaceRepository;
            _applicationSettings = applicationSettings?.Value;
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
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _interfaceRepository.GetAsync(dataAm.Id);

            if (existing != null)
            {
                var errorText = $"Node '{existing.Name}', with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db";

                throw existing.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

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

        //TODO: This is (temporary) a create/delete method to maintain compatibility with Mimir TypeEditor.
        //TODO: This method need to be rewritten as a proper update method with versioning logic.
        public async Task<InterfaceLibCm> UpdateInterface(InterfaceLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update an interface without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update an interface when dataAm is null.");

            if (id == dataAm.Id)
                throw new MimirorgBadRequestException("Not allowed to update: Name, RdsCode or Aspect.");

            var existingDm = await _interfaceRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgNotFoundException($"Interface with id {id} does not exist.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be updated.");

            var created = await CreateInterface(dataAm);

            if (created?.Id != null)
                throw new MimirorgBadRequestException($"Unable to update interface with id {id}");

            return await DeleteInterface(id) ? created : throw new MimirorgBadRequestException($"Unable to delete interface with id {id}");
        }

        public async Task<bool> DeleteInterface(string id)
        {
            var dm = await _interfaceRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The interface with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _interfaceRepository.Context.SaveChangesAsync();
            return status == 1;
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