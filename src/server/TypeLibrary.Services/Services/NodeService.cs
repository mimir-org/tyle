using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
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
        private readonly ITimedHookService _hookService;

        public NodeService(IMapper mapper, INodeRepository nodeRepository, ITimedHookService hookService)
        {
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _hookService = hookService;
        }

        /// <summary>
        /// Get the latest version of a node based on given id
        /// </summary>
        /// <param name="id">The id of the node</param>
        /// <returns>The latest version of the node of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no node with the given id, and that node is at the latest version.</exception>
        public NodeLibCm GetLatestVersion(string id)
        {
            var node = GetLatestVersions().FirstOrDefault(x => x.Id == id);

            if (node == null)
                throw new MimirorgNotFoundException($"There is no node with id {id}");

            return node;
        }

        /// <summary>
        /// Get the latest node versions
        /// </summary>
        /// <returns>A collection of nodes</returns>
        public IEnumerable<NodeLibCm> GetLatestVersions()
        {
            var nodes = _nodeRepository.Get().LatestVersion().ToList();
            nodes = nodes.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            
            foreach (var node in nodes)
                node.Children = nodes.Where(x => x.ParentId == node.Id).ToList();

            return !nodes.Any() ? new List<NodeLibCm>() : _mapper.Map<List<NodeLibCm>>(nodes);
        }

        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="node">The node that should be created</param>
        /// <param name="resetVersion">Would you reset version and first version id?</param>
        /// <returns>The created node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if node is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if node already exist</exception>
        /// <remarks>Remember that creating a new node could be creating a new version of existing node.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<NodeLibCm> Create(NodeLibAm node, bool resetVersion)
        {
            if(node == null) 
                throw new ArgumentNullException(nameof(node));

            var validation = node.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Node is not valid.", validation);

            // Version is included in generating id. It must run before check of already exist. 
            if (resetVersion)
            {
                node.FirstVersionId = node.Id;
                node.Version = "1.0";
            }

            if (await _nodeRepository.Exist(node.Id))
                throw new MimirorgDuplicateException($"Node '{node.Name}' and version '{node.Version}' already exist.");

            var dm = _mapper.Map<NodeLibDm>(node);
            dm.State = State.Draft;

            await _nodeRepository.Create(dm);
            _nodeRepository.ClearAllChangeTrackers();

            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);
            return _mapper.Map<NodeLibCm>(dm);
        }

        /// <summary>
        /// Update a node if the data is allowed to be changed.
        /// </summary>
        /// <param name="node">The node to update</param>
        /// <returns>The updated node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the node does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<NodeLibCm> Update(NodeLibAm node)
        {
            var validation = node.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Node is not valid.", validation);

            var nodeToUpdate = _nodeRepository.Get()
                .LatestVersion()
                .FirstOrDefault(x => x.Id == node.Id);

            if (nodeToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(NodeLibAm.Name), nameof(NodeLibAm.Version) },
                    $"Node with name {node.Name}, aspect {node.Aspect}, Rds Code {node.RdsCode}, id {node.Id} and version {node.Version} does not exist.");
                throw new MimirorgBadRequestException("Node does not exist. Update is not possible.", validation);
            }

            // Get version
            validation = nodeToUpdate.HasIllegalChanges(node);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = nodeToUpdate.CalculateVersionStatus(node);
            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(nodeToUpdate.Id);

            var oldId = node.Id;

            node.FirstVersionId = nodeToUpdate.FirstVersionId;
            node.Version = versionStatus switch
            {
                VersionStatus.Minor => nodeToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => nodeToUpdate.Version.IncrementMajorVersion(),
                _ => nodeToUpdate.Version
            };

            var cm = await Create(node, false);
            await _nodeRepository.ChangeParentId(oldId, cm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);
            return GetLatestVersion(cm.Id);
        }

        /// <summary>
        /// Change node state
        /// </summary>
        /// <param name="id">The node id that should change the state</param>
        /// <param name="state">The new node state</param>
        /// <returns>Node with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the node does not exist on latest version</exception>
        public async Task<NodeLibCm> UpdateState(string id, State state)
        {
            var nodeToUpdate = _nodeRepository.Get()
                .LatestVersion()
                .FirstOrDefault(x => x.Id == id);

            if (nodeToUpdate == null)
                throw new MimirorgNotFoundException($"Node with id {id} does not exist, update is not possible.");

            await _nodeRepository.ChangeState(state, new List<string> { id });
            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);
            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get node existing company id for terminal by id
        /// </summary>
        /// <param name="id">The node id</param>
        /// <returns>Company id for node</returns>
        public async Task<int> GetCompanyId(string id)
        {
            return await _nodeRepository.HasCompany(id);
        }
    }
}