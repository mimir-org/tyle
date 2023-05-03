using AutoMapper;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Data.Constants;

namespace TypeLibrary.Services.Services;

public class AspectObjectService : IAspectObjectService
{
    private readonly IMapper _mapper;
    private readonly IEfAspectObjectRepository _aspectObjectRepository;
    private readonly IAttributeRepository _attributeRepository;
    private readonly IEfAspectObjectTerminalRepository _aspectObjectTerminalRepository;
    private readonly IEfAspectObjectAttributeRepository _aspectObjectAttributeRepository;
    private readonly IAttributeService _attributeService;
    private readonly ITerminalService _terminalService;
    private readonly IRdsService _rdsService;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly ILogger<AspectObjectService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public AspectObjectService(IMapper mapper, IEfAspectObjectRepository aspectObjectRepository, IAttributeRepository attributeRepository, IEfAspectObjectTerminalRepository aspectObjectTerminalRepository, IEfAspectObjectAttributeRepository aspectObjectAttributeRepository, ITerminalService terminalService, IAttributeService attributeService, IRdsService rdsService, ITimedHookService hookService, ILogService logService, ILogger<AspectObjectService> logger, IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _aspectObjectRepository = aspectObjectRepository;
        _attributeRepository = attributeRepository;
        _aspectObjectTerminalRepository = aspectObjectTerminalRepository;
        _aspectObjectAttributeRepository = aspectObjectAttributeRepository;
        _terminalService = terminalService;
        _attributeService = attributeService;
        _rdsService = rdsService;
        _hookService = hookService;
        _logService = logService;
        _logger = logger;
        _contextAccessor = contextAccessor;
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

        dm.FirstVersionId ??= dm.Id;
        dm.State = State.Draft;

        foreach (var aspectObjectTerminal in dm.AspectObjectTerminals)
        {
            aspectObjectTerminal.AspectObjectId = dm.Id;
        }

        dm.AspectObjectAttributes = new List<AspectObjectAttributeLibDm>();
        if (aspectObjectAm.Attributes != null)
        {
            foreach (var attributeId in aspectObjectAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                    _logger.LogError(
                        $"Could not add attribute with id {attributeId} to aspect object with id {dm.Id}, attribute not found.");
                else
                    dm.AspectObjectAttributes.Add(new AspectObjectAttributeLibDm()
                    {
                        AspectObjectId = dm.Id,
                        AttributeId = attribute.Id
                    });
            }
        }

        var createdAspectObject = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        await _logService.CreateLog(createdAspectObject, LogType.Create, createdAspectObject?.State.ToString(), createdAspectObject?.CreatedBy);
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return Get(createdAspectObject?.Id);
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

        var aspectObjectToUpdate = _aspectObjectRepository.FindBy(x => x.Id == id, false)
            .Include(x => x.AspectObjectTerminals).Include(x => x.Attributes).AsSplitQuery().FirstOrDefault();

        if (aspectObjectToUpdate == null)
        {
            throw new MimirorgNotFoundException("Aspect object not found. Update is not possible.");
        }

        if (aspectObjectToUpdate.State != State.Approved && aspectObjectToUpdate.State != State.Draft)
        {
            throw new MimirorgInvalidOperationException("Update can only be performed on aspect object drafts or approved aspect objects.");
        }

        // If the aspect object we want to update is approved, we want to make sure it is the latest version of this object
        // If not, a draft already exists, or it is not the latest approved version of the object
        if (aspectObjectToUpdate.State == State.Approved)
        {
            var latestVersion = _aspectObjectRepository.Get().LatestVersionExcludeDeleted(aspectObjectToUpdate.FirstVersionId);
            if (latestVersion.Id != aspectObjectToUpdate.Id)
                throw new MimirorgInvalidOperationException($"Cannot create new version draft for this object, a draft or newer approved version already exists (id: {latestVersion.Id}).");
        }

        aspectObjectAm.Version = CalculateVersion(aspectObjectAm, aspectObjectToUpdate);

        AspectObjectLibCm aspectObjectToReturn;

        if (aspectObjectToUpdate.State == State.Approved)
        {
            aspectObjectToReturn = await CreateNewDraft(aspectObjectAm, aspectObjectToUpdate);
        }
        else
        {
            aspectObjectToReturn = await UpdateDraft(aspectObjectAm, aspectObjectToUpdate);
        }


        _aspectObjectRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return aspectObjectToReturn;
    }

    private string CalculateVersion(AspectObjectLibAm aspectObjectAm, AspectObjectLibDm aspectObjectToUpdate)
    {
        var latestApprovedVersion =
            _aspectObjectRepository.Get().LatestVersionApproved(aspectObjectToUpdate.FirstVersionId);

        if (latestApprovedVersion == null)
        {
            return "1.0";
        }

        var validation = latestApprovedVersion.HasIllegalChanges(aspectObjectAm);
        if (!validation.IsValid)
            throw new MimirorgInvalidOperationException(validation.Message);

        var versionStatus = latestApprovedVersion.CalculateVersionStatus(aspectObjectAm);

        return versionStatus switch
        {
            VersionStatus.Major => latestApprovedVersion.Version.IncrementMajorVersion(),
            _ => latestApprovedVersion.Version.IncrementMinorVersion(),
        };
    }

    private async Task<AspectObjectLibCm> CreateNewDraft(AspectObjectLibAm aspectObjectAm, AspectObjectLibDm aspectObjectToUpdate)
    {
        var dm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        dm.State = State.Draft;
        dm.FirstVersionId = aspectObjectToUpdate.FirstVersionId;

        foreach (var aspectObjectTerminal in dm.AspectObjectTerminals)
        {
            aspectObjectTerminal.AspectObjectId = dm.Id;
        }

        dm.AspectObjectAttributes = new List<AspectObjectAttributeLibDm>();
        if (aspectObjectAm.Attributes != null)
        {
            foreach (var attributeId in aspectObjectAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                    _logger.LogError(
                        $"Could not add attribute with id {attributeId} to aspect object with id {dm.Id}, attribute not found.");
                else
                    dm.AspectObjectAttributes.Add(new AspectObjectAttributeLibDm()
                    {
                        AspectObjectId = dm.Id,
                        AttributeId = attribute.Id
                    });
            }
        }

        _aspectObjectRepository.Detach(aspectObjectToUpdate);
        var createdAspectObject = await _aspectObjectRepository.Create(dm);
        await _logService.CreateLog(createdAspectObject, LogType.Create, createdAspectObject?.State.ToString(), createdAspectObject?.CreatedBy);

        return Get(createdAspectObject?.Id);
    }

    private async Task<AspectObjectLibCm> UpdateDraft(AspectObjectLibAm aspectObjectAm, AspectObjectLibDm aspectObjectToUpdate)
    {
        aspectObjectToUpdate.Name = aspectObjectAm.Name;
        aspectObjectToUpdate.TypeReference = aspectObjectAm.TypeReference;
        aspectObjectToUpdate.Version = aspectObjectAm.Version;
        aspectObjectToUpdate.Aspect = aspectObjectAm.Aspect;
        aspectObjectToUpdate.PurposeName = aspectObjectAm.PurposeName;
        aspectObjectToUpdate.RdsId = aspectObjectAm.RdsId;
        aspectObjectToUpdate.Symbol = aspectObjectAm.Symbol;
        aspectObjectToUpdate.Description = aspectObjectAm.Description;

        var tempDm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);
        aspectObjectToUpdate.SelectedAttributePredefined = tempDm.SelectedAttributePredefined;

        aspectObjectToUpdate.AspectObjectTerminals ??= new List<AspectObjectTerminalLibDm>();
        aspectObjectToUpdate.Attributes ??= new List<AttributeLibDm>();
        aspectObjectToUpdate.AspectObjectAttributes ??= new List<AspectObjectAttributeLibDm>();

        tempDm.AspectObjectTerminals ??= new List<AspectObjectTerminalLibDm>();

        // Delete removed terminal connections, and add new terminal connections
        var currentAspectObjectTerminals = aspectObjectToUpdate.AspectObjectTerminals.ToHashSet();
        var newAspectObjectTerminals = tempDm.AspectObjectTerminals.ToHashSet();
        foreach (var aspectObjectTerminal in currentAspectObjectTerminals.ExceptBy(
                     newAspectObjectTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            var aspectObjectTerminalDb = _aspectObjectTerminalRepository.FindBy(x =>
                    x.AspectObjectId == aspectObjectToUpdate.Id && x.TerminalId == aspectObjectTerminal.TerminalId && x.ConnectorDirection == aspectObjectTerminal.ConnectorDirection)
                .FirstOrDefault();
            if (aspectObjectTerminalDb == null) continue;
            await _aspectObjectTerminalRepository.Delete(aspectObjectTerminalDb.Id);
        }
        foreach (var aspectObjectTerminal in newAspectObjectTerminals.ExceptBy(
                     currentAspectObjectTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            aspectObjectToUpdate.AspectObjectTerminals.Add(aspectObjectTerminal);
        }

        // Delete removed attributes, and add new attributes
        var currentAttributes = aspectObjectToUpdate.Attributes.ToHashSet();
        var newAttributes = new HashSet<AttributeLibDm>();
        if (aspectObjectAm.Attributes != null)
        {
            foreach (var attributeId in aspectObjectAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                    _logger.LogError(
                        $"Could not add attribute with id {attributeId} to aspect object with id {aspectObjectToUpdate.Id}, attribute not found.");
                else
                    newAttributes.Add(attribute);
            }
        }
        foreach (var attribute in currentAttributes.ExceptBy(newAttributes.Select(x => x.Id), y => y.Id))
        {
            var aspectObjectAttribute = _aspectObjectAttributeRepository.FindBy(x => x.AspectObjectId == aspectObjectToUpdate.Id && x.AttributeId == attribute.Id).FirstOrDefault();
            if (aspectObjectAttribute == null) continue;
            await _aspectObjectAttributeRepository.Delete(aspectObjectAttribute.Id);
        }
        foreach (var attribute in newAttributes.ExceptBy(currentAttributes.Select(x => x.Id), y => y.Id))
        {
            aspectObjectToUpdate.AspectObjectAttributes.Add(new AspectObjectAttributeLibDm
            {
                AspectObjectId = aspectObjectToUpdate.Id,
                AttributeId = attribute.Id
            });
        }

        await _aspectObjectTerminalRepository.SaveAsync();
        await _aspectObjectAttributeRepository.SaveAsync();
        await _aspectObjectRepository.SaveAsync();
        var updatedBy = !string.IsNullOrWhiteSpace(_contextAccessor.GetName()) ? _contextAccessor.GetName() : CreatedBy.Unknown;
        await _logService.CreateLog(aspectObjectToUpdate, LogType.Update, aspectObjectToUpdate.State.ToString(), updatedBy);

        return Get(aspectObjectToUpdate.Id);
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

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State change on approved aspect object with id {id} is not allowed.");

        if (state == State.Approve)
        {
            var latestApprovedVersion =
                _aspectObjectRepository.Get().LatestVersionApproved(dm.FirstVersionId);

            if (latestApprovedVersion != null && latestApprovedVersion.Equals(dm))
            {
                throw new MimirorgInvalidOperationException("Cannot approve this aspect object since it is identical to the currently approved version.");
            }

            if (dm.Rds.State != State.Approved)
            {
                if (dm.Rds.State == State.Deleted) throw new MimirorgInvalidOperationException("Cannot request approval for aspect object that uses deleted RDS.");

                await _rdsService.ChangeState(dm.RdsId, State.Approve);
            }

            foreach (var attribute in dm.Attributes)
            {
                if (attribute.State == State.Approved) continue;
                if (attribute.State == State.Deleted) throw new MimirorgInvalidOperationException("Cannot request approval for aspect object that uses deleted attributes.");

                await _attributeService.ChangeState(attribute.Id, State.Approve);
            }

            foreach (var aspectObjectTerminal in dm.AspectObjectTerminals)
            {
                var terminal = _terminalService.Get(aspectObjectTerminal.TerminalId);

                if (terminal.State == State.Approved) continue;
                if (terminal.State == State.Deleted) throw new MimirorgInvalidOperationException("Cannot request approval for aspect object that uses deleted terminals.");

                await _terminalService.ChangeState(terminal.Id, State.Approve);
            }
        }
        else if (state == State.Approved)
        {
            if (dm.Rds.State != State.Approved)
                throw new MimirorgInvalidOperationException("Cannot approve aspect object that uses unapproved RDS.");
            if (dm.Attributes.Any(attribute => attribute.State != State.Approved))
                throw new MimirorgInvalidOperationException("Cannot approve aspect object that uses unapproved attributes.");
            if (dm.AspectObjectTerminals.Select(x => x.Terminal).Any(terminal => terminal.State != State.Approved))
                throw new MimirorgInvalidOperationException("Cannot approve aspect object that uses unapproved terminals.");
        }

        await _aspectObjectRepository.ChangeState(state, dm.Id);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), dm.CreatedBy);
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return new ApprovalDataCm
        {
            Id = id,
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