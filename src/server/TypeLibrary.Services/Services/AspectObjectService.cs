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

namespace TypeLibrary.Services.Services;

public class AspectObjectService : IAspectObjectService
{
    private readonly IMapper _mapper;
    private readonly IAspectObjectRepository _aspectObjectRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;

    public AspectObjectService(IMapper mapper, IAspectObjectRepository aspectObjectRepository, ITimedHookService hookService, ILogService logService)
    {
        _mapper = mapper;
        _aspectObjectRepository = aspectObjectRepository;
        _hookService = hookService;
        _logService = logService;
    }

    /// <summary>
    /// Get the latest version of a node based on given id
    /// </summary>
    /// <param name="id">The id of the node</param>
    /// <returns>The latest version of the node of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no node with the given id, and that node is at the latest version.</exception>
    public AspectObjectLibCm Get(int id)
    {
        var dm = _aspectObjectRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Node with id {id} not found.");

        return _mapper.Map<AspectObjectLibCm>(dm);
    }

    /// <summary>
    /// Get the latest node versions
    /// </summary>
    /// <returns>A collection of nodes</returns>
    public IEnumerable<AspectObjectLibCm> GetLatestVersions()
    {
        var dms = _aspectObjectRepository.Get()?.LatestVersionsExcludeDeleted()?.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

        if (dms == null)
            throw new MimirorgNotFoundException("No nodes were found.");

        foreach (var dm in dms)
            dm.Children = dms.Where(x => x.ParentId == dm.Id).ToList();

        return !dms.Any() ? new List<AspectObjectLibCm>() : _mapper.Map<List<AspectObjectLibCm>>(dms);
    }

    /// <summary>
    /// Create a new node
    /// </summary>
    /// <param name="aspectObjectAm">The node that should be created</param>
    /// <returns>The created node</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if node is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if node already exist</exception>
    /// <remarks>Remember that creating a new node could be creating a new version of existing node.
    /// They will have the same first version id, but have different version and id.</remarks>
    public async Task<AspectObjectLibCm> Create(AspectObjectLibAm aspectObjectAm)
    {
        if (aspectObjectAm == null)
            throw new ArgumentNullException(nameof(aspectObjectAm));

        var validation = aspectObjectAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Node is not valid.", validation);

        aspectObjectAm.Version = "1.0";
        var dm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        dm.State = State.Draft;

        // TODO: This is a temporary fix, since the TS types are not built correctly for nullable ints
        if (dm.ParentId == 0) dm.ParentId = null;

        var createdNode = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdNode, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

        return Get(createdNode.Id);
    }

    /// <summary>
    /// Update a node if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the node to update</param>
    /// <param name="aspectObjectAm">The node to update</param>
    /// <returns>The updated node</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if the node does not exist,
    /// if it is not valid or there are not allowed changes.</exception>
    /// <remarks>ParentId to old references will also be updated.</remarks>
    public async Task<AspectObjectLibCm> Update(int id, AspectObjectLibAm aspectObjectAm)
    {
        var validation = aspectObjectAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Node is not valid.", validation);

        var nodeToUpdate = _aspectObjectRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

        if (nodeToUpdate == null)
        {
            validation = new Validation(new List<string> { nameof(AspectObjectLibAm.Name), nameof(AspectObjectLibAm.Version) },
                $"Node with name {aspectObjectAm.Name}, aspect {aspectObjectAm.Aspect}, Rds Code {aspectObjectAm.RdsCode}, id {id} and version {aspectObjectAm.Version} does not exist.");
            throw new MimirorgBadRequestException("Node does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        validation = nodeToUpdate.HasIllegalChanges(aspectObjectAm);

        if (!validation.IsValid)
            throw new MimirorgBadRequestException(validation.Message, validation);

        var versionStatus = nodeToUpdate.CalculateVersionStatus(aspectObjectAm);

        if (versionStatus == VersionStatus.NoChange)
            return Get(nodeToUpdate.Id);

        //We need to take into account that there exist a higher version that has state 'Deleted'.
        //Therefore we need to increment minor/major from the latest version, including those with state 'Deleted'.
        nodeToUpdate.Version = _aspectObjectRepository.Get().LatestVersionIncludeDeleted(nodeToUpdate.FirstVersionId).Version;

        aspectObjectAm.Version = versionStatus switch
        {
            VersionStatus.Minor => nodeToUpdate.Version.IncrementMinorVersion(),
            VersionStatus.Major => nodeToUpdate.Version.IncrementMajorVersion(),
            _ => nodeToUpdate.Version
        };

        var dm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        dm.State = State.Draft;
        dm.FirstVersionId = nodeToUpdate.FirstVersionId;

        var nodeCm = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        await _aspectObjectRepository.ChangeParentId(id, nodeCm.Id);
        await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

        return Get(nodeCm.Id);
    }

    /// <summary>
    /// Change node state
    /// </summary>
    /// <param name="id">The node id that should change the state</param>
    /// <param name="state">The new node state</param>
    /// <returns>Node with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the node does not exist on latest version</exception>
    public async Task<ApprovalDataCm> ChangeState(int id, State state)
    {
        var dm = _aspectObjectRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Node with id {id} not found, or is not latest version.");

        await _aspectObjectRepository.ChangeState(state, new List<int> { dm.Id });
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectNode);

        return new ApprovalDataCm
        {
            Id = id.ToString(),
            State = state
        };
    }

    /// <summary>
    /// Get node existing company id for terminal by id
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>Company id for node</returns>
    public async Task<int> GetCompanyId(int id)
    {
        return await _aspectObjectRepository.HasCompany(id);
    }
}