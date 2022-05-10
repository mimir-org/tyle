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
    public class NodeService : INodeService
    {
        private readonly IMapper _mapper;
        private readonly INodeRepository _nodeRepository;
        private readonly IRdsRepository _rdsRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleRepository _simpleRepository;
        private readonly IPurposeRepository _purposeRepository;

        public NodeService(IPurposeRepository purposeRepository, IAttributeRepository attributeRepository, IRdsRepository rdsRepository, IMapper mapper, INodeRepository nodeRepository, ISimpleRepository simpleRepository)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _simpleRepository = simpleRepository;
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
            var existingNode = await _nodeRepository.GetAsync(dataAm.Id);

            if (existingNode != null)
                throw new MimirorgBadRequestException($"Node name: '{existingNode.Name}' with RdsCode '{existingNode.RdsCode}', Aspect '{existingNode.Aspect}' and version: {existingNode.Version} already exist");

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

        public Task<NodeLibCm> UpdateNode(NodeLibAm dataAm, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteNode(string id)
        {
            var dm = await _nodeRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is already marked as deleted in the database.");

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