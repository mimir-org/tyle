using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

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
    private readonly IEmailService _emailService;

    public AspectObjectService(IMapper mapper, IEfAspectObjectRepository aspectObjectRepository, IAttributeRepository attributeRepository, IEfAspectObjectTerminalRepository aspectObjectTerminalRepository, IEfAspectObjectAttributeRepository aspectObjectAttributeRepository, ITerminalService terminalService, IAttributeService attributeService, IRdsService rdsService, ITimedHookService hookService, ILogService logService, ILogger<AspectObjectService> logger, IHttpContextAccessor contextAccessor, IEmailService emailService)
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
        _emailService = emailService;
    }

    /// <inheritdoc />
    public IEnumerable<AspectObjectLibCm> GetLatestVersions()
    {
        var latestAll = _aspectObjectRepository.Get()?.LatestVersions()?.ToList() ?? new List<AspectObjectLibDm>();
        var latestApproved = _aspectObjectRepository.Get()?.LatestVersionsApproved()?.ToList() ?? new List<AspectObjectLibDm>();

        var result = latestAll.Union(latestApproved).OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

        return !result.Any() ? new List<AspectObjectLibCm>() : _mapper.Map<List<AspectObjectLibCm>>(result);
    }

    /// <inheritdoc />
    public AspectObjectLibCm Get(string id)
    {
        var dm = _aspectObjectRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Aspect object with id {id} not found.");

        return _mapper.Map<AspectObjectLibCm>(dm);
    }

    /// <inheritdoc />
    public AspectObjectLibCm GetLatestApproved(string id)
    {
        var givenAspectObject = _aspectObjectRepository.Get(id);

        if (givenAspectObject == null)
            throw new MimirorgNotFoundException($"Aspect object with id {id} not found.");

        var allVersions = _aspectObjectRepository.GetAllVersions(givenAspectObject);
        var latestVersionApproved = allVersions.LatestVersionApproved(givenAspectObject.FirstVersionId);

        if (latestVersionApproved == null)
            throw new MimirorgNotFoundException($"No approved version was found for aspect object with id {id}.");

        return _mapper.Map<AspectObjectLibCm>(latestVersionApproved);
    }

    /// <inheritdoc />
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
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to aspect object with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.AspectObjectAttributes.Add(new AspectObjectAttributeLibDm { AspectObjectId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }

        var createdAspectObject = await _aspectObjectRepository.Create(dm);
        _aspectObjectRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);
        await _logService.CreateLog(createdAspectObject, LogType.Create, createdAspectObject?.State.ToString(), createdAspectObject?.CreatedBy);

        return Get(createdAspectObject?.Id);
    }

    /// <inheritdoc />
    public async Task<AspectObjectLibCm> Update(string id, AspectObjectLibAm aspectObjectAm)
    {
        var validation = aspectObjectAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Aspect object is not valid.", validation);

        var aspectObjectToUpdate = _aspectObjectRepository.FindBy(x => x.Id == id, false).Include(x => x.AspectObjectTerminals).Include(x => x.Attributes).AsSplitQuery().FirstOrDefault();

        if (aspectObjectToUpdate == null)
            throw new MimirorgNotFoundException("Aspect object not found. Update is not possible.");

        if (aspectObjectToUpdate.State != State.Approved && aspectObjectToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on aspect object drafts or approved aspect objects.");

        /* If the aspect object we want to update is approved, we want to make sure it is the latest version of this object
           If not, a draft already exists, or it is not the latest approved version of the object */
        if (aspectObjectToUpdate.State == State.Approved)
        {
            var latestVersion = _aspectObjectRepository.Get().LatestVersion(aspectObjectToUpdate.FirstVersionId);

            if (latestVersion.Id != aspectObjectToUpdate.Id)
                throw new MimirorgInvalidOperationException($"Cannot create new version draft for this object, a draft or newer approved version already exists (id: {latestVersion.Id}).");
        }

        aspectObjectAm.Version = CalculateVersion(aspectObjectAm, aspectObjectToUpdate);

        AspectObjectLibCm aspectObjectToReturn;

        if (aspectObjectToUpdate.State == State.Approved)
            aspectObjectToReturn = await CreateNewDraft(aspectObjectAm, aspectObjectToUpdate);
        else
            aspectObjectToReturn = await UpdateDraft(aspectObjectAm, aspectObjectToUpdate);

        _aspectObjectRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);

        return aspectObjectToReturn;
    }

    private string CalculateVersion(AspectObjectLibAm aspectObjectAm, AspectObjectLibDm aspectObjectToUpdate)
    {
        var latestApprovedVersion = _aspectObjectRepository.Get().LatestVersionApproved(aspectObjectToUpdate.FirstVersionId);

        if (latestApprovedVersion == null)
            return "1.0";

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
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to aspect object with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.AspectObjectAttributes.Add(new AspectObjectAttributeLibDm { AspectObjectId = dm.Id, AttributeId = attribute.Id });
                }
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
        aspectObjectToUpdate.PurposeName = aspectObjectAm.PurposeName;
        aspectObjectToUpdate.RdsId = aspectObjectAm.RdsId;
        aspectObjectToUpdate.Symbol = aspectObjectAm.Symbol;
        aspectObjectToUpdate.Description = aspectObjectAm.Description;

        if (aspectObjectToUpdate.Version == VersionConstant.OnePointZero)
        {
            aspectObjectToUpdate.Aspect = aspectObjectAm.Aspect;
            aspectObjectToUpdate.CompanyId = aspectObjectAm.CompanyId;
        }

        var tempDm = _mapper.Map<AspectObjectLibDm>(aspectObjectAm);

        aspectObjectToUpdate.SelectedAttributePredefined = tempDm.SelectedAttributePredefined;
        aspectObjectToUpdate.AspectObjectTerminals ??= new List<AspectObjectTerminalLibDm>();
        aspectObjectToUpdate.Attributes ??= new List<AttributeLibDm>();
        aspectObjectToUpdate.AspectObjectAttributes ??= new List<AspectObjectAttributeLibDm>();

        tempDm.AspectObjectTerminals ??= new List<AspectObjectTerminalLibDm>();

        // Delete removed terminal connections, and add new terminal connections
        var currentAspectObjectTerminals = aspectObjectToUpdate.AspectObjectTerminals.ToHashSet();
        var newAspectObjectTerminals = tempDm.AspectObjectTerminals.ToHashSet();

        foreach (var aspectObjectTerminal in currentAspectObjectTerminals.ExceptBy(newAspectObjectTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            var aspectObjectTerminalDb = _aspectObjectTerminalRepository
                .FindBy(x => x.AspectObjectId == aspectObjectToUpdate.Id
                 && x.TerminalId == aspectObjectTerminal.TerminalId
                 && x.ConnectorDirection == aspectObjectTerminal.ConnectorDirection).FirstOrDefault();

            if (aspectObjectTerminalDb == null)
                continue;

            await _aspectObjectTerminalRepository.Delete(aspectObjectTerminalDb.Id);
        }

        foreach (var aspectObjectTerminal in newAspectObjectTerminals.ExceptBy(currentAspectObjectTerminals.Select(x => x.GetHash()), y => y.GetHash()))
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
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to aspect object with id {aspectObjectToUpdate.Id}, attribute not found.");
                }
                else
                {
                    newAttributes.Add(attribute);
                }
            }
        }

        foreach (var attribute in currentAttributes.ExceptBy(newAttributes.Select(x => x.Id), y => y.Id))
        {
            var aspectObjectAttribute = _aspectObjectAttributeRepository.FindBy(x => x.AspectObjectId == aspectObjectToUpdate.Id && x.AttributeId == attribute.Id).FirstOrDefault();

            if (aspectObjectAttribute == null)
                continue;

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
        await _logService.CreateLog(aspectObjectToUpdate, LogType.Update, aspectObjectToUpdate.State.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        return Get(aspectObjectToUpdate.Id);
    }

    /// <inheritdoc />
    public async Task Delete(string id)
    {
        var dm = _aspectObjectRepository.Get(id) ?? throw new MimirorgNotFoundException($"Aspect object with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved aspect object with id {id}.");

        await _aspectObjectRepository.Delete(id);
    }

    /// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _aspectObjectRepository.Get(id) ?? throw new MimirorgNotFoundException($"Aspect object with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        if (state == State.Review)
        {
            var latestApprovedVersion = _aspectObjectRepository.Get().LatestVersionApproved(dm.FirstVersionId);

            if (latestApprovedVersion != null && latestApprovedVersion.Equals(dm))
                throw new MimirorgInvalidOperationException("Cannot approve this aspect object since it is identical to the currently approved version.");

            if (dm.Rds.State != State.Approved)
            {
                await _rdsService.ChangeState(dm.RdsId, State.Review, true);
            }

            foreach (var attribute in dm.Attributes)
            {
                if (attribute.State == State.Approved)
                    continue;

                await _attributeService.ChangeState(attribute.Id, State.Review, true);
            }

            foreach (var aspectObjectTerminal in dm.AspectObjectTerminals)
            {
                var terminal = _terminalService.Get(aspectObjectTerminal.TerminalId);

                if (terminal.State == State.Approved)
                    continue;

                await _terminalService.ChangeState(terminal.Id, State.Review, true);
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
        _hookService.HookQueue.Enqueue(CacheKey.AspectObject);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), dm.CreatedBy);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.AspectObject);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }

    /// <inheritdoc />
    public int GetCompanyId(string id)
    {
        return _aspectObjectRepository.HasCompany(id);
    }
}