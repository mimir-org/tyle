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

            var interfaceLibCm = _mapper.Map<InterfaceLibCm>(data);

            if (interfaceLibCm == null)
                throw new MimirorgMappingException("InterfaceLibDm", "InterfaceLibCm");

            return interfaceLibCm;
        }

        public Task<IEnumerable<InterfaceLibCm>> GetInterfaces()
        {
            var interfaces = _interfaceRepository.GetAllInterfaces().ToList();
            var interfaceLibCms = _mapper.Map<IEnumerable<InterfaceLibCm>>(interfaces);

            if (interfaces.Any() && (interfaceLibCms == null || !interfaceLibCms.Any()))
                throw new MimirorgMappingException("List<InterfaceLibDm>", "ICollection<InterfaceLibAm>");

            return Task.FromResult(interfaceLibCms ?? new List<InterfaceLibCm>());
        }

        public Task<InterfaceLibCm> UpdateInterface(InterfaceLibAm dataAm, string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<InterfaceLibCm> CreateInterface(InterfaceLibAm dataAm)
        {
            var existingInterface = await _interfaceRepository.GetAsync(dataAm.Id);

            if (existingInterface != null)
                throw new MimirorgBadRequestException($"There is already registered an interface with name: {existingInterface.Name} with version: {existingInterface.Version}");

            var interfaceLibDm = _mapper.Map<InterfaceLibDm>(dataAm);

            if (interfaceLibDm == null)
                throw new MimirorgMappingException("InterfaceLibAm", "InterfaceLibDm");

            interfaceLibDm.RdsName = _rdsRepository.FindBy(x => x.Id == interfaceLibDm.RdsId)?.First()?.Name;
            interfaceLibDm.PurposeName = _purposeRepository.FindBy(x => x.Id == interfaceLibDm.PurposeId)?.First()?.Name;

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

        public async Task<bool> DeleteInterface(string id)
        {
            await _interfaceRepository.Delete(id);
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