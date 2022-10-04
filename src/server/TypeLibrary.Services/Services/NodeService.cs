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
            var nodeCm = GetLatestVersions().FirstOrDefault(x => x.Id == id);

            if (nodeCm == null)
                throw new MimirorgNotFoundException($"There is no node with id {id}");

            return nodeCm;
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
        /// <param name="nodeAm">The node that should be created</param>
        /// <returns>The created node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if node is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if node already exist</exception>
        /// <remarks>Remember that creating a new node could be creating a new version of existing node.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<NodeLibCm> Create(NodeLibAm nodeAm)
        {
            if (nodeAm == null)
                throw new ArgumentNullException(nameof(nodeAm));

            var validation = nodeAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Node is not valid.", validation);

            if (await _nodeRepository.Exist(nodeAm.Id))
                throw new MimirorgDuplicateException($"Node '{nodeAm.Name}' and version '{nodeAm.Version}' already exist.");

            nodeAm.Version = "1.0";
            var dm = _mapper.Map<NodeLibDm>(nodeAm);

            dm.State = State.Draft;

            await _nodeRepository.Create(dm);
            _nodeRepository.ClearAllChangeTrackers();
            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

            return GetLatestVersion(dm.Id);
        }

        /// <summary>
        /// Update a node if the data is allowed to be changed.
        /// </summary>
        /// <param name="nodeAm">The node to update</param>
        /// <returns>The updated node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the node does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<NodeLibCm> Update(NodeLibAm nodeAm)
        {
            var validation = nodeAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Node is not valid.", validation);

            var nodeToUpdate = _nodeRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == nodeAm.Id);

            if (nodeToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(NodeLibAm.Name), nameof(NodeLibAm.Version) },
                    $"Node with name {nodeAm.Name}, aspect {nodeAm.Aspect}, Rds Code {nodeAm.RdsCode}, id {nodeAm.Id} and version {nodeAm.Version} does not exist.");

                throw new MimirorgBadRequestException("Node does not exist. Update is not possible.", validation);
            }

            validation = nodeToUpdate.HasIllegalChanges(nodeAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = nodeToUpdate.CalculateVersionStatus(nodeAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(nodeToUpdate.Id);

            nodeAm.Version = versionStatus switch
            {
                VersionStatus.Minor => nodeToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => nodeToUpdate.Version.IncrementMajorVersion(),
                _ => nodeToUpdate.Version
            };

            var nodeDm = _mapper.Map<NodeLibDm>(nodeAm);

            nodeDm.FirstVersionId = nodeToUpdate.FirstVersionId;
            nodeDm.State = State.Draft;

            var nodeCm = await _nodeRepository.Create(nodeDm);
            _nodeRepository.ClearAllChangeTrackers();

            await _nodeRepository.ChangeParentId(nodeAm.Id, nodeCm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

            return GetLatestVersion(nodeCm.Id);
        }

        /// <summary>
        /// Change node state
        /// </summary>
        /// <param name="id">The node id that should change the state</param>
        /// <param name="state">The new node state</param>
        /// <returns>Node with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the node does not exist on latest version</exception>
        public async Task<NodeLibCm> ChangeState(string id, State state)
        {
            var dm = _nodeRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if(dm == null)
                throw new MimirorgNotFoundException($"Node with id {id} not found, or is not latest version.");

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