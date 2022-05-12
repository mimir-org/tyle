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
    public class NodeService : INodeService
    {
        private readonly IMapper _mapper;
        private readonly IEfNodeRepository _nodeRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEFSimpleRepository _simpleRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public NodeService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IMapper mapper, IEfNodeRepository nodeRepository, IEFSimpleRepository simpleRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _simpleRepository = simpleRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<NodeLibCm> GetNode(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get node. The id is missing value.");

            var data = await _nodeRepository.FindNode(id).FirstOrDefaultAsync();

            if (data == null)
                throw new MimirorgNotFoundException($"There is no node with id: {id}");

            if (data.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var nodeLibCm = _mapper.Map<NodeLibCm>(data);

            if (nodeLibCm == null)
                throw new MimirorgMappingException("NodeLibDm", "NodeLibCm");

            return nodeLibCm;
        }

        public Task<IEnumerable<NodeLibCm>> GetNodes()
        {
            var nodes = _nodeRepository.GetAllNodes().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var nodeLibCms = _mapper.Map<IEnumerable<NodeLibCm>>(nodes);

            if (nodes.Any() && (nodeLibCms == null || !nodeLibCms.Any()))
                throw new MimirorgMappingException("List<NodeLibDm>", "ICollection<NodeLibAm>");

            return Task.FromResult(nodeLibCms ?? new List<NodeLibCm>());
        }

        public async Task<NodeLibCm> CreateNode(NodeLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var dm = await _nodeRepository.GetAsync(dataAm.Id);

            if (dm != null)
            {
                var errorText = $"Node '{dm.Name}' with RdsCode '{dm.RdsCode}', Aspect '{dm.Aspect}' and version '{dm.Version}' already exist in db";

                throw dm.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

            var nodeLibDm = _mapper.Map<NodeLibDm>(dataAm);

            if (nodeLibDm == null)
                throw new MimirorgMappingException("NodeLibAm", "NodeLibDm");

            if (nodeLibDm.Attributes != null && nodeLibDm.Attributes.Any())
                _attributeRepository.Attach(nodeLibDm.Attributes, EntityState.Unchanged);

            if (nodeLibDm.Simples != null && nodeLibDm.Simples.Any())
                _simpleRepository.Attach(nodeLibDm.Simples, EntityState.Unchanged);

            await _nodeRepository.CreateAsync(nodeLibDm);
            await _nodeRepository.SaveAsync();

            if (nodeLibDm.Simples != null && nodeLibDm.Simples.Any())
                _simpleRepository.Detach(nodeLibDm.Simples);

            if (nodeLibDm.Attributes != null && nodeLibDm.Attributes.Any())
                _attributeRepository.Detach(nodeLibDm.Attributes);

            _nodeRepository.Detach(nodeLibDm);

            return await GetNode(nodeLibDm.Id);
        }

        //TODO: This is (temporary) a create/delete method to maintain compatibility with Mimir TypeEditor.
        //TODO: This method need to be rewritten as a proper update method with versioning logic.
        public async Task<NodeLibCm> UpdateNode(NodeLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update a node without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update a node when dataAm is null.");

            var existingDm = await _nodeRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgNotFoundException($"Node with id {id} does not exist.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be updated.");

            var created = await CreateNode(dataAm);

            if (created?.Id != null)
                throw new MimirorgBadRequestException($"Unable to update node with id {id}");

            return await DeleteNode(id) ? created : throw new MimirorgBadRequestException($"Unable to delete node with id {id}");
        }

        public async Task<bool> DeleteNode(string id)
        {
            var dm = await _nodeRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The node with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _nodeRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _nodeRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _simpleRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}