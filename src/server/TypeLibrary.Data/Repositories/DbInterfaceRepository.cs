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
    public class DbInterfaceRepository : IInterfaceRepository
    {
        private readonly IEfAttributeRepository _efAttributeRepository;
        private readonly IEfInterfaceRepository _efInterfaceRepository;
        private readonly ApplicationSettings _applicationSettings;

        public DbInterfaceRepository(IEfAttributeRepository efAttributeRepository, IEfInterfaceRepository efInterfaceRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _efAttributeRepository = efAttributeRepository;
            _efInterfaceRepository = efInterfaceRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<InterfaceLibDm> Get()
        {
            return _efInterfaceRepository.GetAllInterfaces().Where(x => !x.Deleted);
        }

        public async Task<InterfaceLibDm> Get(string id)
        {
            return await _efInterfaceRepository.FindInterface(id).FirstOrDefaultAsync(x => !x.Deleted);
        }

        public async Task Create(InterfaceLibDm dataDm)
        {
            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _efAttributeRepository.Attach(dataDm.Attributes, EntityState.Unchanged);

            await _efInterfaceRepository.CreateAsync(dataDm);
            await _efInterfaceRepository.SaveAsync();

            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _efAttributeRepository.Detach(dataDm.Attributes);

            _efInterfaceRepository.Detach(dataDm);

            ClearAllChangeTrackers();
        }

        public async Task<bool> Delete(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _efInterfaceRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _efAttributeRepository?.Context?.ChangeTracker.Clear();
            _efInterfaceRepository?.Context?.ChangeTracker.Clear();
        }
    }
}