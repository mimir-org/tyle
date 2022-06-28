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
    public class DbNodeRepository : INodeRepository
    {
        private readonly IEfAttributeRepository _efAttributeRepository;
        private readonly IEfNodeRepository _efNodeRepository;
        private readonly IEfSimpleRepository _efSimpleRepository;
        private readonly ApplicationSettings _applicationSettings;

        public DbNodeRepository(IEfAttributeRepository efAttributeRepository, IOptions<ApplicationSettings> applicationSettings, IEfNodeRepository efNodeRepository, IEfSimpleRepository efSimpleRepository)
        {
            _efAttributeRepository = efAttributeRepository;
            _efNodeRepository = efNodeRepository;
            _efSimpleRepository = efSimpleRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<NodeLibDm> Get()
        {
            return _efNodeRepository.GetAllNodes().Where(x => !x.Deleted);
        }

        public async Task<NodeLibDm> Get(string id)
        {
            return await _efNodeRepository.FindNode(id).FirstOrDefaultAsync(x => !x.Deleted);
        }

        public async Task Create(NodeLibDm dataDm)
        {
            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _efAttributeRepository.Attach(dataDm.Attributes, EntityState.Unchanged);

            if (dataDm.Simples != null && dataDm.Simples.Any())
                _efSimpleRepository.Attach(dataDm.Simples, EntityState.Unchanged);

            await _efNodeRepository.CreateAsync(dataDm);
            await _efNodeRepository.SaveAsync();

            if (dataDm.Simples != null && dataDm.Simples.Any())
                _efSimpleRepository.Detach(dataDm.Simples);

            if (dataDm.Attributes != null && dataDm.Attributes.Any())
                _efAttributeRepository.Detach(dataDm.Attributes);

            _efNodeRepository.Detach(dataDm);

            ClearAllChangeTrackers();
        }

        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgNotFoundException("Id can't be null or empty.");

            var dm = await _efNodeRepository.Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Node with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            _efNodeRepository.Update(dm);
            var status = await _efNodeRepository.SaveAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _efAttributeRepository?.Context?.ChangeTracker.Clear();
            _efNodeRepository?.Context?.ChangeTracker.Clear();
            _efSimpleRepository?.Context?.ChangeTracker.Clear();
        }
    }
}