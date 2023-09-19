using AutoMapper;
using Lucene.Net.Util;
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

public class BlockService : IBlockService
{
    private readonly IMapper _mapper;
    private readonly IEfBlockRepository _blockRepository;
    private readonly IAttributeRepository _attributeRepository;
    private readonly IAttributeGroupRepository _attributeGroupRepository;
    private readonly IEfBlockTerminalRepository _blockTerminalRepository;
    private readonly IEfBlockAttributeRepository _blockAttributeRepository;
    private readonly IAttributeService _attributeService;
    private readonly ITerminalService _terminalService;
    private readonly IRdsService _rdsService;
    private readonly ITimedHookService _hookService;
    private readonly ILogService _logService;
    private readonly ILogger<BlockService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;

    public BlockService(IMapper mapper, IEfBlockRepository blockRepository, IAttributeRepository attributeRepository, IAttributeGroupRepository attributeGroupRepository, IEfBlockTerminalRepository blockTerminalRepository, IEfBlockAttributeRepository blockAttributeRepository, ITerminalService terminalService, IAttributeService attributeService, IRdsService rdsService, ITimedHookService hookService, ILogService logService, ILogger<BlockService> logger, IHttpContextAccessor contextAccessor, IEmailService emailService)
    {
        _mapper = mapper;
        _blockRepository = blockRepository;
        _attributeRepository = attributeRepository;
        _attributeGroupRepository = attributeGroupRepository;
        _blockTerminalRepository = blockTerminalRepository;
        _blockAttributeRepository = blockAttributeRepository;
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
    public IEnumerable<BlockLibCm> GetLatestVersions()
    {
        var latestAll = _blockRepository.Get()?.LatestVersions()?.ToList() ?? new List<BlockLibDm>();
        var latestApproved = _blockRepository.Get()?.LatestVersionsApproved()?.ToList() ?? new List<BlockLibDm>();

        var result = latestAll.Union(latestApproved).OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

        return !result.Any() ? new List<BlockLibCm>() : _mapper.Map<List<BlockLibCm>>(result);
    }

    /// <inheritdoc />
    public BlockLibCm Get(string id)
    {
        var dm = _blockRepository.Get(id);

        if (dm == null)
            throw new MimirorgNotFoundException($"Block with id {id} not found.");

        return _mapper.Map<BlockLibCm>(dm);
    }

    /// <inheritdoc />
    public BlockLibCm GetLatestApproved(string id)
    {
        var givenBlock = _blockRepository.Get(id);

        if (givenBlock == null)
            throw new MimirorgNotFoundException($"Block with id {id} not found.");

        var allVersions = _blockRepository.GetAllVersions(givenBlock);
        var latestVersionApproved = allVersions.LatestVersionApproved(givenBlock.FirstVersionId);

        if (latestVersionApproved == null)
            throw new MimirorgNotFoundException($"No approved version was found for block with id {id}.");

        return _mapper.Map<BlockLibCm>(latestVersionApproved);
    }

    /// <inheritdoc />
    public async Task<BlockLibCm> Create(BlockLibAm blockAm)
    {
        if (blockAm == null)
            throw new ArgumentNullException(nameof(blockAm));

        var validation = blockAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Block is not valid.", validation);

        blockAm.Version = "1.0";
        var dm = _mapper.Map<BlockLibDm>(blockAm);

        dm.FirstVersionId ??= dm.Id;
        dm.State = State.Draft;

        foreach (var blockTerminal in dm.BlockTerminals)
        {
            blockTerminal.BlockId = dm.Id;
        }

        dm.BlockAttributes = new List<BlockAttributeLibDm>();

        if (blockAm.Attributes != null)
        {
            foreach (var attributeId in blockAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);

                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.BlockAttributes.Add(new BlockAttributeLibDm { BlockId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }

        if (blockAm.AttributeGroups != null)
        {
            foreach (var item in blockAm.AttributeGroups)
            {
                var currentAttributeGroup = _attributeGroupRepository.GetSingleAttributeGroup(item);

                if (currentAttributeGroup == null)
                {
                    _logger.LogError($"Could not add attribute group with id {item} to block with id {dm.Id}, attribute not found.");
                }
                else
                {

                    foreach (var attributeGroupItem in currentAttributeGroup.AttributeGroupAttributes)
                    {
                        dm.BlockAttributes.Add(new BlockAttributeLibDm { BlockId = dm.Id, AttributeId = attributeGroupItem.AttributeId, PartOfAttributeGroup = item });
                    }
                }
            }
        }

        var createdBlock = await _blockRepository.Create(dm);
        _blockRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Block);
        await _logService.CreateLog(createdBlock, LogType.Create, createdBlock?.State.ToString(), createdBlock?.CreatedBy);

        return Get(createdBlock?.Id);
    }

    /// <inheritdoc />
    public async Task<BlockLibCm> Update(string id, BlockLibAm blockAm)
    {
        var validation = blockAm.ValidateObject();

        if (!validation.IsValid)
            throw new MimirorgBadRequestException("Block is not valid.", validation);

        var blockToUpdate = _blockRepository.FindBy(x => x.Id == id, false).Include(x => x.BlockTerminals).Include(x => x.BlockAttributes).AsSplitQuery().FirstOrDefault();

        if (blockToUpdate == null)
            throw new MimirorgNotFoundException("Block not found. Update is not possible.");

        if (blockToUpdate.State != State.Approved && blockToUpdate.State != State.Draft)
            throw new MimirorgInvalidOperationException("Update can only be performed on block drafts or approved blocks.");

        /* If the block we want to update is approved, we want to make sure it is the latest version of this object
           If not, a draft already exists, or it is not the latest approved version of the object */
        if (blockToUpdate.State == State.Approved)
        {
            var latestVersion = _blockRepository.Get().LatestVersion(blockToUpdate.FirstVersionId);

            if (latestVersion.Id != blockToUpdate.Id)
                throw new MimirorgInvalidOperationException($"Cannot create new version draft for this object, a draft or newer approved version already exists (id: {latestVersion.Id}).");
        }

        blockAm.Version = CalculateVersion(blockAm, blockToUpdate);

        BlockLibCm blockToReturn;

        if (blockToUpdate.State == State.Approved)
            blockToReturn = await CreateNewDraft(blockAm, blockToUpdate);
        else
            blockToReturn = await UpdateDraft(blockAm, blockToUpdate);

        _blockRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Block);

        return blockToReturn;
    }

    private string CalculateVersion(BlockLibAm blockAm, BlockLibDm blockToUpdate)
    {
        var latestApprovedVersion = _blockRepository.Get().LatestVersionApproved(blockToUpdate.FirstVersionId);

        if (latestApprovedVersion == null)
            return "1.0";

        var validation = latestApprovedVersion.HasIllegalChanges(blockAm);

        if (!validation.IsValid)
            throw new MimirorgInvalidOperationException(validation.Message);

        var versionStatus = latestApprovedVersion.CalculateVersionStatus(blockAm);

        return versionStatus switch
        {
            VersionStatus.Major => latestApprovedVersion.Version.IncrementMajorVersion(),
            _ => latestApprovedVersion.Version.IncrementMinorVersion(),
        };
    }

    private async Task<BlockLibCm> CreateNewDraft(BlockLibAm blockAm, BlockLibDm blockToUpdate)
    {
        var dm = _mapper.Map<BlockLibDm>(blockAm);

        dm.State = State.Draft;
        dm.FirstVersionId = blockToUpdate.FirstVersionId;

        foreach (var blockTerminal in dm.BlockTerminals)
        {
            blockTerminal.BlockId = dm.Id;
        }

        dm.BlockAttributes = new List<BlockAttributeLibDm>();

        if (blockAm.Attributes != null)
        {
            foreach (var attributeId in blockAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);
                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {dm.Id}, attribute not found.");
                }
                else
                {
                    dm.BlockAttributes.Add(new BlockAttributeLibDm { BlockId = dm.Id, AttributeId = attribute.Id });
                }
            }
        }

        _blockRepository.Detach(blockToUpdate);
        var createdBlock = await _blockRepository.Create(dm);
        await _logService.CreateLog(createdBlock, LogType.Create, createdBlock?.State.ToString(), createdBlock?.CreatedBy);

        return Get(createdBlock?.Id);
    }

    private async Task<BlockLibCm> UpdateDraft(BlockLibAm blockAm, BlockLibDm blockToUpdate)
    {
        blockToUpdate.Name = blockAm.Name;
        blockToUpdate.TypeReference = blockAm.TypeReference;
        blockToUpdate.Version = blockAm.Version;
        blockToUpdate.PurposeName = blockAm.PurposeName;
        blockToUpdate.RdsId = blockAm.RdsId;
        blockToUpdate.Symbol = blockAm.Symbol;
        blockToUpdate.Description = blockAm.Description;

        if (blockToUpdate.Version == VersionConstant.OnePointZero)
        {
            blockToUpdate.Aspect = blockAm.Aspect;
            blockToUpdate.CompanyId = blockAm.CompanyId;
        }

        var tempDm = _mapper.Map<BlockLibDm>(blockAm);

        blockToUpdate.SelectedAttributePredefined = tempDm.SelectedAttributePredefined;
        blockToUpdate.BlockTerminals ??= new List<BlockTerminalLibDm>();
        blockToUpdate.BlockAttributes ??= new List<BlockAttributeLibDm>();

        tempDm.BlockTerminals ??= new List<BlockTerminalLibDm>();

        // Delete removed terminal connections, and add new terminal connections
        var currentBlockTerminals = blockToUpdate.BlockTerminals.ToHashSet();
        var newBlockTerminals = tempDm.BlockTerminals.ToHashSet();

        foreach (var blockTerminal in currentBlockTerminals.ExceptBy(newBlockTerminals.Select(x => x.GetHash()), y => y.GetHash()))
        {
            var blockTerminalDb = _blockTerminalRepository
                .FindBy(x => x.BlockId == blockToUpdate.Id
                 && x.TerminalId == blockTerminal.TerminalId
                 && x.ConnectorDirection == blockTerminal.ConnectorDirection).FirstOrDefault();

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
        var newBlockAttributes = new HashSet<BlockAttributeLibDm>();

        if (blockAm.Attributes != null)
        {
            foreach (var attributeId in blockAm.Attributes)
            {
                var attribute = _attributeRepository.Get(attributeId);

                if (attribute == null)
                {
                    _logger.LogError($"Could not add attribute with id {attributeId} to block with id {blockToUpdate.Id}, attribute not found.");
                }
                else
                {
                    newBlockAttributes.Add(new BlockAttributeLibDm { BlockId = blockToUpdate.Id, AttributeId = attribute.Id });
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
            blockToUpdate.BlockAttributes.Add(new BlockAttributeLibDm
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
    }

    /// <inheritdoc />
    public async Task Delete(string id)
    {
        var dm = _blockRepository.Get(id) ?? throw new MimirorgNotFoundException($"Block with id {id} not found.");

        if (dm.State == State.Approved)
            throw new MimirorgInvalidOperationException($"Can't delete approved block with id {id}.");

        await _blockRepository.Delete(id);
        await _blockRepository.SaveAsync();
    }

    /// <inheritdoc />
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
    }
}