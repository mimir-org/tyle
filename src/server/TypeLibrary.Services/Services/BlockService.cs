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
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Data.Repositories.Ef;

namespace TypeLibrary.Services.Services;

public class BlockService : IBlockService
{
    private readonly IMapper _mapper;
    private readonly IEfBlockRepository _blockRepository;
    private readonly IAttributeRepository _attributeRepository;
    private readonly IEfBlockTerminalRepository _blockTerminalRepository;
    private readonly IEfBlockAttributeRepository _blockAttributeRepository;
    private readonly IAttributeService _attributeService;
    private readonly ITerminalService _terminalService;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly ILogger<BlockService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;
    private readonly IEfBlockClassifierRepository _blockClassifierRepository;
    private readonly IEfClassifierRepository _classifierRepository;
    private readonly IEfPurposeRepository _purposeRepository;

    public BlockService(IMapper mapper, IEfBlockRepository blockRepository, IAttributeRepository attributeRepository, IEfBlockTerminalRepository blockTerminalRepository, IEfBlockAttributeRepository blockAttributeRepository, ITerminalService terminalService, IAttributeService attributeService, ITimedHookService hookService, ILogService logService, ILogger<BlockService> logger, IHttpContextAccessor contextAccessor, IEmailService emailService, IEfBlockClassifierRepository blockClassifierRepository, IEfClassifierRepository classifierRepository, IEfPurposeRepository purposeRepository)
    {
        _mapper = mapper;
        _blockRepository = blockRepository;
        _attributeRepository = attributeRepository;
        _blockTerminalRepository = blockTerminalRepository;
        _blockAttributeRepository = blockAttributeRepository;
        _terminalService = terminalService;
        _attributeService = attributeService;
        _hookService = hookService;
        _logService = logService;
        _logger = logger;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
        _blockClassifierRepository = blockClassifierRepository;
        _classifierRepository = classifierRepository;
        _purposeRepository = purposeRepository;
    }

    /// <inheritdoc />
    public IEnumerable<BlockTypeView> GetLatestVersions()
    {
        /*var latestAll = _blockRepository.Get()?.LatestVersions()?.ToList() ?? new List<BlockType>();
        var latestApproved = _blockRepository.Get()?.LatestVersionsApproved()?.ToList() ?? new List<BlockType>();

        var result = latestAll.Union(latestApproved).OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase);*/

        var result = _blockRepository.Get();

        return !result.Any() ? new List<BlockTypeView>() : _mapper.Map<List<BlockTypeView>>(result);
    }

    /// <inheritdoc />
    public BlockTypeView Get(Guid id)
    {
        var dm = _blockRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Block with id {id} not found.");

        return _mapper.Map<BlockTypeView>(dm);
    }

    /*/// <inheritdoc />
    public BlockTypeView GetLatestApproved(string id)
    {
        var givenBlock = _blockRepository.Get(id);

        if (givenBlock == null)
            throw new MimirorgNotFoundException($"Block with id {id} not found.");

        var allVersions = _blockRepository.GetAllVersions(givenBlock);
        var latestVersionApproved = allVersions.LatestVersionApproved(givenBlock.FirstVersionId);

        if (latestVersionApproved == null)
            throw new MimirorgNotFoundException($"No approved version was found for block with id {id}.");

        return _mapper.Map<BlockTypeView>(latestVersionApproved);
    }*/

    /// <inheritdoc />
    public async Task<BlockTypeView> Create(BlockTypeRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        //var validation = request.ValidateObject();

        //if (!validation.IsValid)
            //throw new MimirorgBadRequestException("Block is not valid.", validation);

        //request.Version = "1.0";
        var dm = new BlockType(request.Name, request.Description, _contextAccessor.GetUserId());

        await SetBlockTypeFields(dm, request);

        //dm.FirstVersionId ??= dm.Id;
        //dm.State = State.Draft;

        /*dm.BlockAttributes = new List<BlockAttributeTypeReference>();

        if (request.Attributes != null)
        {
            foreach (var attributeId in request.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);

                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.BlockAttributes.Add(new BlockAttributeTypeReference { BlockId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }*/

        var createdBlock = await _blockRepository.Create(dm);
        _blockRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Block);
        //await _logService.CreateLog(createdBlock, LogType.Create, createdBlock?.State.ToString(), createdBlock?.CreatedBy);

        return Get(createdBlock.Id);
    }

    /// <inheritdoc />
    public async Task<BlockTypeView> Update(Guid id, BlockTypeRequest request)
    {
        /*var validation = request.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Block is not valid.", validation);*/

        var blockToUpdate = _blockRepository.Get(id);

        if (blockToUpdate == null)
            throw new MimirorgNotFoundException("Block not found. Update is not possible.");

        /*if (blockToUpdate.State != State.Approved && blockToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on block drafts or approved blocks.");

        // If the block we want to update is approved, we want to make sure it is the latest version of this object
        // If not, a draft already exists, or it is not the latest approved version of the object
        if (blockToUpdate.State == State.Approved)
        {
            var latestVersion = _blockRepository.Get().LatestVersion(blockToUpdate.FirstVersionId);

            if (latestVersion.Id != blockToUpdate.Id)
                throw new MimirorgInvalidOperationException($"Cannot create new version draft for this object, a draft or newer approved version already exists (id: {latestVersion.Id}).");
        }

        request.Version = CalculateVersion(request, blockToUpdate);

        BlockTypeView blockToReturn;

        if (blockToUpdate.State == State.Approved)
            blockToReturn = await CreateNewDraft(request, blockToUpdate);
        else
            blockToReturn = await UpdateDraft(request, blockToUpdate);*/

        blockToUpdate.Name = request.Name;
        blockToUpdate.Description = request.Description;
        if (blockToUpdate.CreatedBy != _contextAccessor.GetUserId())
            blockToUpdate.ContributedBy.Add(_contextAccessor.GetUserId());
        blockToUpdate.LastUpdateOn = DateTimeOffset.Now;

        await SetBlockTypeFields(blockToUpdate, request);

        _blockRepository.Update(blockToUpdate);
        await _blockClassifierRepository.SaveAsync();
        await _blockTerminalRepository.SaveAsync();
        await _blockAttributeRepository.SaveAsync();
        await _blockRepository.SaveAsync();

        _blockRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Block);

        return Get(blockToUpdate.Id);
    }

    /*private string CalculateVersion(BlockTypeRequest request, BlockType blockToUpdate)
    {
        var latestApprovedVersion = _blockRepository.Get().LatestVersionApproved(blockToUpdate.FirstVersionId);

        if (latestApprovedVersion == null)
            return "1.0";

        var validation = latestApprovedVersion.HasIllegalChanges(request);

        if (!validation.IsValid)
            throw new MimirorgInvalidOperationException(validation.Message);

        var versionStatus = latestApprovedVersion.CalculateVersionStatus(request);

        return versionStatus switch
        {
            VersionStatus.Major => latestApprovedVersion.Version.IncrementMajorVersion(),
            _ => latestApprovedVersion.Version.IncrementMinorVersion(),
        };
    }

    private async Task<BlockTypeView> CreateNewDraft(BlockTypeRequest request, BlockType blockToUpdate)
    {
        var dm = _mapper.Map<BlockType>(request);

        dm.State = State.Draft;
        dm.FirstVersionId = blockToUpdate.FirstVersionId;

        foreach (var blockTerminal in dm.BlockTerminals)
        {
            blockTerminal.BlockId = dm.Id;
        }

        dm.BlockAttributes = new List<BlockAttributeTypeReference>();

        if (request.Attributes != null)
        {
            foreach (var attributeId in request.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.BlockAttributes.Add(new BlockAttributeTypeReference { BlockId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }

        _blockRepository.Detach(blockToUpdate);
        var createdBlock = await _blockRepository.Create(dm);
        await _logService.CreateLog(createdBlock, LogType.Create, createdBlock?.State.ToString(), createdBlock?.CreatedBy);

        return Get(createdBlock?.Id);
    }

    private async Task<BlockTypeView> UpdateDraft(BlockTypeRequest request, BlockType blockToUpdate)
    {
        blockToUpdate.Name = request.Name;
        blockToUpdate.TypeReference = request.TypeReference;
        blockToUpdate.Version = request.Version;
        blockToUpdate.PurposeName = request.PurposeName;
        blockToUpdate.RdsId = request.RdsId;
        blockToUpdate.Symbol = request.Symbol;
        blockToUpdate.Description = request.Description;

        if (blockToUpdate.Version == VersionConstant.OnePointZero)
        {
            blockToUpdate.Aspect = request.Aspect;
            blockToUpdate.CompanyId = request.CompanyId;
        }

        var tempDm = _mapper.Map<BlockType>(request);

        blockToUpdate.SelectedAttributePredefined = tempDm.SelectedAttributePredefined;
        blockToUpdate.BlockTerminals ??= new List<BlockTerminalTypeReference>();
        blockToUpdate.BlockAttributes ??= new List<BlockAttributeTypeReference>();

        tempDm.BlockTerminals ??= new List<BlockTerminalTypeReference>();

        // Delete removed terminal connections, and add new terminal connections
        var currentBlockTerminals = blockToUpdate.BlockTerminals.ToHashSet();
        var newBlockTerminals = tempDm.BlockTerminals.ToHashSet();

        foreach (var blockTerminal in currentBlockTerminals.ExceptBy(newBlockTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            var blockTerminalDb = _blockTerminalRepository
                .FindBy(x => x.BlockId == blockToUpdate.Id
                 && x.TerminalId == blockTerminal.TerminalId
                 && x.Direction == blockTerminal.Direction).FirstOrDefault();

            if (blockTerminalDb == null)
                continue;

            await _blockTerminalRepository.Delete(blockTerminalDb.Id);
        }

        foreach (var blockTerminal in newBlockTerminals.ExceptBy(currentBlockTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            blockToUpdate.BlockTerminals.Add(blockTerminal);
        }

        // Delete removed attributes, and add new attributes
        var currentBlockAttributes = blockToUpdate.BlockAttributes.ToHashSet();
        var newBlockAttributes = new HashSet<BlockAttributeTypeReference>();

        if (request.Attributes != null)
        {
            foreach (var attributeId in request.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);

                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {blockToUpdate.Id}, attribute not found.");
                }
                else
                {
                    newBlockAttributes.Add(new BlockAttributeTypeReference { BlockId = blockToUpdate.Id, AttributeId = attribute.Id });
                }
            }
        }

        foreach (var blockAttribute in currentBlockAttributes.ExceptBy(newBlockAttributes.Select(x => x.AttributeId), y => y.AttributeId))
        {
            var blockAttributeToDelete = _blockAttributeRepository.FindBy(x => x.BlockId == blockToUpdate.Id && x.AttributeId == blockAttribute.AttributeId).FirstOrDefault();

            if (blockAttributeToDelete == null)
                continue;

            await _blockAttributeRepository.Delete(blockAttributeToDelete.Id);
        }

        foreach (var blockAttribute in newBlockAttributes.ExceptBy(currentBlockAttributes.Select(x => x.AttributeId), y => y.AttributeId))
        {
            blockToUpdate.BlockAttributes.Add(new BlockAttributeTypeReference
            {
                BlockId = blockToUpdate.Id,
                AttributeId = blockAttribute.AttributeId
            });
        }

        await _blockTerminalRepository.SaveAsync();
        await _blockAttributeRepository.SaveAsync();
        await _blockRepository.SaveAsync();
        await _logService.CreateLog(blockToUpdate, LogType.Update, blockToUpdate.State.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        return Get(blockToUpdate.Id);
    }*/

    /// <inheritdoc />
    public async Task Delete(Guid id)
    {
        var dm = _blockRepository.Get(id) ?? throw new MimirorgNotFoundException($"Block with id {id} not found.");

        //if (dm.State == State.Approved)
        //    throw new MimirorgInvalidOperationException($"Can't delete approved block with id {id}.");

        await _blockRepository.Delete(id);
        await _blockRepository.SaveAsync();
    }

    /*/// <inheritdoc />
    public async Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
    {
        var dm = _blockRepository.Get(id) ?? throw new MimirorgNotFoundException($"Block with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"State '{state}' is not allowed for object {dm.Name} with id {dm.Id} since current state is {dm.State}");

        if (state == State.Review)
        {
            var latestApprovedVersion = _blockRepository.Get().LatestVersionApproved(dm.FirstVersionId);

            if (latestApprovedVersion != null && latestApprovedVersion.Equals(dm))
                throw new MimirorgInvalidOperationException("Cannot approve this block since it is identical to the currently approved version.");

            if (dm.RdsId == null)
                throw new MimirorgInvalidOperationException("Cannot approve a block without a RDS code.");

            if (dm.Rds.State != State.Approved)
            {
                await _rdsService.ChangeState(dm.RdsId, State.Review, true);
            }

            foreach (var attribute in dm.BlockAttributes.Select(x => x.Attribute))
            {
                if (attribute.State == State.Approved)
                    continue;

                await _attributeService.ChangeState(attribute.Id, State.Review, true);
            }

            foreach (var blockTerminal in dm.BlockTerminals)
            {
                var terminal = _terminalService.Get(blockTerminal.TerminalId);

                if (terminal.State == State.Approved)
                    continue;

                await _terminalService.ChangeState(terminal.Id, State.Review, true);
            }
        }
        else if (state == State.Approved)
        {
            if (dm.Rds.State != State.Approved)
                throw new MimirorgInvalidOperationException("Cannot approve block that uses unapproved RDS.");

            if (dm.BlockAttributes.Select(x => x.Attribute).Any(attribute => attribute.State != State.Approved))
                throw new MimirorgInvalidOperationException("Cannot approve block that uses unapproved attributes.");

            if (dm.BlockTerminals.Select(x => x.Terminal).Any(terminal => terminal.State != State.Approved))
                throw new MimirorgInvalidOperationException("Cannot approve block that uses unapproved terminals.");
        }

        await _blockRepository.ChangeState(state, dm.Id);
        _hookService.HookQueue.Enqueue(CacheKey.Block);
        await _logService.CreateLog(dm, LogType.State, state.ToString(), _contextAccessor.GetUserId() ?? CreatedBy.Unknown);

        if (sendStateEmail)
            await _emailService.SendObjectStateEmail(dm.Id, state, dm.Name, ObjectTypeName.Block);

        return new ApprovalDataCm { Id = dm.Id, State = state };
    }

    /// <inheritdoc />
    public int GetCompanyId(string id)
    {
        return _blockRepository.HasCompany(id);
    }*/

    private async Task SetBlockTypeFields(BlockType dm, BlockTypeRequest request)
    {
        var classifiersToRemove = new List<BlockClassifierMapping>();

        foreach (var classifier in dm.Classifiers)
        {
            if (request.ClassifierReferenceIds.Contains(classifier.ClassifierId)) continue;

            classifiersToRemove.Add(classifier);
            await _blockClassifierRepository.Delete(classifier.Id);
        }

        foreach (var classifierToRemove in classifiersToRemove)
        {
            dm.Classifiers.Remove(classifierToRemove);
        }

        foreach (var classifierReferenceId in request.ClassifierReferenceIds)
        {
            if (dm.Classifiers.Select(x => x.ClassifierId).Contains(classifierReferenceId)) continue;

            var classifier = await _classifierRepository.GetAsync(classifierReferenceId) ??
                             throw new MimirorgBadRequestException(
                                 $"No classifier reference with id {classifierReferenceId} found.");
            dm.Classifiers.Add(new BlockClassifierMapping(dm.Id, classifierReferenceId));
        }

        if (request.PurposeReferenceId != null)
        {
            var purpose = await _purposeRepository.GetAsync((int) request.PurposeReferenceId) ??
                          throw new MimirorgBadRequestException(
                              $"No purpose reference with id {request.PurposeReferenceId} found.");
            dm.PurposeId = purpose.Id;
        }
        else
        {
            dm.PurposeId = null;
            dm.Purpose = null;
        }

        dm.Notation = request.Notation;
        dm.Symbol = request.Symbol;
        dm.Aspect = request.Aspect;

        var terminalsToRemove = new List<BlockTerminalTypeReference>();

        foreach (var terminal in dm.BlockTerminals)
        {
            if (request.BlockTerminals.Any(x => x.TerminalId == terminal.TerminalId && x.Direction == terminal.Direction)) continue;

            terminalsToRemove.Add(terminal);
            await _blockTerminalRepository.Delete(terminal.Id);
        }

        foreach (var terminalToRemove in terminalsToRemove)
        {
            dm.BlockTerminals.Remove(terminalToRemove);
        }

        foreach (var blockTerminal in request.BlockTerminals)
        {
            if (dm.BlockTerminals.Any(x => x.TerminalId == blockTerminal.TerminalId && x.Direction == blockTerminal.Direction))
            {
                var savedBlockTerminal =
                    _blockTerminalRepository.FindBy(x => x.BlockId == dm.Id && x.TerminalId == blockTerminal.TerminalId && x.Direction == blockTerminal.Direction, false).FirstOrDefault();
                savedBlockTerminal!.MinCount = blockTerminal.MinCount;
                savedBlockTerminal.MaxCount = blockTerminal.MaxCount;
            }
            else
            {
                dm.BlockTerminals.Add(new BlockTerminalTypeReference(dm.Id, blockTerminal.TerminalId, blockTerminal.Direction, blockTerminal.MinCount, blockTerminal.MaxCount));
            }
        }

        var attributesToRemove = new List<BlockAttributeTypeReference>();

        foreach (var attribute in dm.BlockAttributes)
        {
            if (request.BlockAttributes.Any(x => x.AttributeId == attribute.AttributeId)) continue;

            attributesToRemove.Add(attribute);
            await _blockAttributeRepository.Delete(attribute.Id);
        }

        foreach (var attributeToRemove in attributesToRemove)
        {
            dm.BlockAttributes.Remove(attributeToRemove);
        }

        foreach (var blockAttribute in request.BlockAttributes)
        {
            if (dm.BlockAttributes.Any(x => x.AttributeId == blockAttribute.AttributeId))
            {
                var savedBlockAttribute =
                    _blockAttributeRepository.FindBy(x => x.BlockId == dm.Id && x.AttributeId == blockAttribute.AttributeId, false).FirstOrDefault();
                savedBlockAttribute!.MinCount = blockAttribute.MinCount;
                savedBlockAttribute.MaxCount = blockAttribute.MaxCount;
            }
            else
            {
                dm.BlockAttributes.Add(new BlockAttributeTypeReference(dm.Id, blockAttribute.AttributeId, blockAttribute.MinCount, blockAttribute.MaxCount));
            }
        }
    }
}