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
        private readonly ILogService _logService;

        public NodeService(IMapper mapper, INodeRepository nodeRepository, ITimedHookService hookService, ILogService logService)
        {
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _hookService = hookService;
            _logService = logService;
        }

        /// <summary>
        /// Get the latest version of a node based on given id
        /// </summary>
        /// <param name="id">The id of the node</param>
        /// <returns>The latest version of the node of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no node with the given id, and that node is at the latest version.</exception>
        public NodeLibCm GetLatestVersion(string id)
        {
            var dm = _nodeRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Node with id {id} not found.");

            return _mapper.Map<NodeLibCm>(dm);
        }

        /// <summary>
        /// Get the latest node versions
        /// </summary>
        /// <returns>A collection of nodes</returns>
        public IEnumerable<NodeLibCm> GetLatestVersions()
        {
            var dms = _nodeRepository.Get()?.LatestVersionsExcludeDeleted()?.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (dms == null)
                throw new MimirorgNotFoundException("No nodes were found.");

            foreach (var dm in dms)
                dm.Children = dms.Where(x => x.ParentId == dm.Id).ToList();

            return !dms.Any() ? new List<NodeLibCm>() : _mapper.Map<List<NodeLibCm>>(dms);
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
            dm.FirstVersionId = dm.Id;

            await _nodeRepository.Create(dm);
            _nodeRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
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

            var nodeToUpdate = _nodeRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == nodeAm.Id);

            if (nodeToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(NodeLibAm.Name), nameof(NodeLibAm.Version) },
                    $"Node with name {nodeAm.Name}, aspect {nodeAm.Aspect}, Rds Code {nodeAm.RdsCode}, id {nodeAm.Id} and version {nodeAm.Version} does not exist.");
                throw new MimirorgBadRequestException("Node does not exist or is flagged as deleted. Update is not possible.", validation);
            }

            validation = nodeToUpdate.HasIllegalChanges(nodeAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = nodeToUpdate.CalculateVersionStatus(nodeAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(nodeToUpdate.Id);

            //We need to take into account that there exist a higher version that has state 'Deleted'.
            //Therefore we need to increment minor/major from the latest version, including those with state 'Deleted'.
            nodeToUpdate.Version = _nodeRepository.Get().LatestVersionIncludeDeleted(nodeToUpdate.FirstVersionId).Version;

            nodeAm.Version = versionStatus switch
            {
                VersionStatus.Minor => nodeToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => nodeToUpdate.Version.IncrementMajorVersion(),
                _ => nodeToUpdate.Version
            };

            var dm = _mapper.Map<NodeLibDm>(nodeAm);

            dm.State = State.Draft;
            dm.FirstVersionId = nodeToUpdate.FirstVersionId;

            var nodeCm = await _nodeRepository.Create(dm);
            _nodeRepository.ClearAllChangeTrackers();
            await _nodeRepository.ChangeParentId(nodeAm.Id, nodeCm.Id);
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
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
        public async Task<ApprovalDataCm> ChangeState(string id, State state)
        {
            var dm = _nodeRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Node with id {id} not found, or is not latest version.");

            await _nodeRepository.ChangeState(state, new List<string> { dm.Id });
            await _logService.CreateLog(dm, LogType.State, state.ToString());
            _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

            return new ApprovalDataCm
            {
                Id = id,
                State = state
            };
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