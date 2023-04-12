using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
    private readonly IAttributeRepository _attributeRepository;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly ILogger<AspectObjectService> _logger;
    private readonly IApplicationSettingsRepository _settings;

    public AspectObjectService(IMapper mapper, IAspectObjectRepository aspectObjectRepository, IAttributeRepository attributeRepository, ITimedHookService hookService, ILogService logService, ILogger<AspectObjectService> logger, IApplicationSettingsRepository settings)
    {
        _mapper = mapper;
        _aspectObjectRepository = aspectObjectRepository;
        _attributeRepository = attributeRepository;
        _hookService = hookService;
        _logService = logService;
        _logger = logger;
        _settings = settings;
    }

    /// <summary>
    /// Get the latest version of an aspect object based on given id
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>The latest version of the aspect object of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no aspect object with the given id, and that aspect object is at the latest version.</exception>
    public AspectObjectLibCm Get(string id)
    {
        var dm = _aspectObjectRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Aspect object with id {id} not found.");

        return _mapper.Map<AspectObjectLibCm>(dm);
    }

    /// <summary>
    /// Get the latest aspect object versions
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    public IEnumerable<AspectObjectLibCm> GetLatestVersions()
    {
        var dms = _aspectObjectRepository.Get()?.LatestVersionsExcludeDeleted()?.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

        if (dms == null)
            throw new MimirorgNotFoundException("No aspect objects were found.");

        foreach (var dm in dms)
            dm.Children = dms.Where(x => x.ParentId == dm.Id).ToList();

        return !dms.Any() ? new List<AspectObjectLibCm>() : _mapper.Map<List<AspectObjectLibCm>>(dms);
    }

    /// <summary>
    /// Create a new aspect object
    /// </summary>
    /// <param name="aspectObjectAm">The aspect object that should be created</param>
    /// <returns>The created aspect object</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if aspect object is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if aspect object already exist</exception>
    /// <remarks>Remember that creating a new aspect object could be creating a new version of existing aspect object.
    /// They will have the same first version id, but have different version and id.</remarks>
    public async Task<AspectObjectLibCm> Create(AspectObjectLibAm aspectObjectAm)
    {
        if (aspectObjectAm == null)
            throw new ArgumentNullException(nameof(aspectObjectAm));

        var validation = aspectObjectAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Aspect object is not valid.", validation);

        aspectObjectAm.Version = "1.0";
        var dm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        dm.Id = Guid.NewGuid().ToString();
        dm.Iri = $"{_settings.ApplicationSemanticUrl}/aspectobject/{dm.Id}";
        dm.FirstVersionId ??= dm.Id;
        dm.State = State.Draft;

        foreach (var aspectObjectTerminal in dm.AspectObjectTerminals)
            aspectObjectTerminal.AspectObjectId = dm.Id;

        dm.Attributes = new List<AttributeLibDm>();
        foreach (var attributeId in aspectObjectAm.Attributes)
        {
            var attribute = _attributeRepository.Get(attributeId);
            if (attribute == null)
                _logger.LogError(
                    $"Could not add attribute with id {attributeId} to aspect object with id {dm.Id}, attribute not found.");
            else
                dm.Attributes.Add(attribute);
        }

        var createdAspectObject = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdAspectObject, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return Get(createdAspectObject.Id);
    }

    /// <summary>
    /// Update an aspect object if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the aspect object to update</param>
    /// <param name="aspectObjectAm">The aspect object to update</param>
    /// <returns>The updated aspect object</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if the aspect object does not exist,
    /// if it is not valid or there are not allowed changes.</exception>
    /// <remarks>ParentId to old references will also be updated.</remarks>
    public async Task<AspectObjectLibCm> Update(string id, AspectObjectLibAm aspectObjectAm)
    {
        var validation = aspectObjectAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Aspect object is not valid.", validation);

        var aspectObjectToUpdate = _aspectObjectRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

        if (aspectObjectToUpdate == null)
        {
            validation = new Validation(new List<string> { nameof(AspectObjectLibAm.Name), nameof(AspectObjectLibAm.Version) },
                $"Aspect object with name {aspectObjectAm.Name}, aspect {aspectObjectAm.Aspect}, Rds Code {aspectObjectAm.RdsCode}, id {id} and version {aspectObjectAm.Version} does not exist.");
            throw new MimirorgBadRequestException("Aspect object does not exist or is flagged as deleted. Update is not possible.", validation);
        }

        validation = aspectObjectToUpdate.HasIllegalChanges(aspectObjectAm);

        if (!validation.IsValid)
            throw new MimirorgBadRequestException(validation.Message, validation);

        var versionStatus = aspectObjectToUpdate.CalculateVersionStatus(aspectObjectAm);

        if (versionStatus == VersionStatus.NoChange)
            return Get(aspectObjectToUpdate.Id);

        //We need to take into account that there exist a higher version that has state 'Deleted'.
        //Therefore we need to increment minor/major from the latest version, including those with state 'Deleted'.
        aspectObjectToUpdate.Version = _aspectObjectRepository.Get().LatestVersionIncludeDeleted(aspectObjectToUpdate.FirstVersionId).Version;

        aspectObjectAm.Version = versionStatus switch
        {
            VersionStatus.Minor => aspectObjectToUpdate.Version.IncrementMinorVersion(),
            VersionStatus.Major => aspectObjectToUpdate.Version.IncrementMajorVersion(),
            _ => aspectObjectToUpdate.Version
        };

        var dm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        dm.State = State.Draft;
        dm.FirstVersionId = aspectObjectToUpdate.FirstVersionId;

        var aspectObjectCm = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        await _aspectObjectRepository.ChangeParentId(id, aspectObjectCm.Id);
        await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return Get(aspectObjectCm.Id);
    }

    /// <summary>
    /// Change aspect object state
    /// </summary>
    /// <param name="id">The aspect object id that should change the state</param>
    /// <param name="state">The new aspect object state</param>
    /// <returns>Aspect objects with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the aspect object does not exist on latest version</exception>
    public async Task<ApprovalDataCm> ChangeState(string id, State state)
    {
        var dm = _aspectObjectRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Aspect object with id {id} not found, or is not latest version.");

        await _aspectObjectRepository.ChangeState(state, dm.Id);
        await _logService.CreateLog(dm, LogType.State, state.ToString());
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return new ApprovalDataCm
        {
            Id = id.ToString(),
            State = state
        };
    }

    /// <summary>
    /// Get aspect object existing company id for terminal by id
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>Company id for aspect object</returns>
    public int GetCompanyId(string id)
    {
        return _aspectObjectRepository.HasCompany(id);
    }
}