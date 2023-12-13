using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using System;
using Tyle.Application.Blocks;
using Tyle.Application.Blocks.Requests;
using Tyle.Application.Common;
using Tyle.Converters;
using Tyle.Core.Blocks;
using Tyle.Core.Common;

namespace Tyle.Persistence.Blocks;

public class BlockRepository : IBlockRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<BlockType> _dbSet;
    private readonly IDownstreamApi _downstreamApi;
    private readonly IJsonLdConversionService _jsonLdConversionService;
    private readonly IUserInformationService _userInformationService;

    public BlockRepository(TyleDbContext context, IDownstreamApi downstreamApi, IJsonLdConversionService jsonLdConversionService, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Blocks;
        _downstreamApi = downstreamApi;
        _jsonLdConversionService = jsonLdConversionService;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<BlockType>> GetAll(State? state = null)
    {
        var query = _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Symbol).ThenInclude(x => x!.ConnectionPoints)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Medium)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .Include(x => x.Terminals).ThenInclude(x => x.ConnectionPoint)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery();

        if (state != null)
        {
            query = query.Where(x => x.State == state);
        }

        return await query.ToListAsync();
    }

    public async Task<BlockType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Symbol).ThenInclude(x => x!.ConnectionPoints)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Medium)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .Include(x => x.Terminals).ThenInclude(x => x.ConnectionPoint)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlockType> Create(BlockTypeRequest request)
    {
        var block = new BlockType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            State = State.Draft,
            PurposeId = request.PurposeId,
            Notation = request.Notation,
            SymbolId = request.SymbolId,
            Aspect = request.Aspect
        };

        block.LastUpdateOn = block.CreatedOn;

        block.Classifiers = request.ClassifierIds.Select(classifierId => new BlockClassifierJoin { BlockId = block.Id, ClassifierId = classifierId }).ToList();

        block.Terminals = request.Terminals.Select(x => new BlockTerminalTypeReference { BlockId = block.Id, TerminalId = x.TerminalId, Direction = x.Direction, MinCount = x.MinCount, MaxCount = x.MaxCount, ConnectionPointId = x.ConnectionPointId }).ToList();

        block.Attributes = request.Attributes.Select(x => new BlockAttributeTypeReference { BlockId = block.Id, AttributeId = x.AttributeId, MinCount = x.MinCount, MaxCount = x.MaxCount }).ToList();

        _dbSet.Add(block);
        await _context.SaveChangesAsync();

        return await Get(block.Id);
    }

    public async Task<BlockType?> Update(Guid id, BlockTypeRequest request)
    {
        var block = await _dbSet.AsTracking()
            .Include(x => x.Classifiers)
            .Include(x => x.Terminals)
            .Include(x => x.Attributes)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (block == null)
        {
            return null;
        }

        if (block.State != State.Draft)
        {
            throw new InvalidOperationException($"Blocks with state '{block.State}' cannot be updated.");
        }

        block.Name = request.Name;
        block.Description = request.Description;
        if (_userInformationService.GetUserId() != block.CreatedBy)
        {
            block.ContributedBy.Add(_userInformationService.GetUserId());
        }

        block.LastUpdateOn = DateTimeOffset.Now;

        var blockClassifiersToRemove = block.Classifiers.Where(x => !request.ClassifierIds.Contains(x.ClassifierId)).ToList();
        foreach (var blockClassifier in blockClassifiersToRemove)
        {
            block.Classifiers.Remove(blockClassifier);
        }

        foreach (var classifierId in request.ClassifierIds)
        {
            if (block.Classifiers.Any(x => x.ClassifierId == classifierId)) continue;

            block.Classifiers.Add(new BlockClassifierJoin
            {
                BlockId = id,
                ClassifierId = classifierId
            });
        }

        block.PurposeId = request.PurposeId;
        block.Notation = request.Notation;
        block.SymbolId = request.SymbolId;
        block.Aspect = request.Aspect;

        var blockTerminalToRemove = block.Terminals.Where(x => !request.Terminals.Any(y => y.TerminalId == x.TerminalId && y.Direction == x.Direction)).ToList();
        foreach (var blockTerminal in blockTerminalToRemove)
        {
            block.Terminals.Remove(blockTerminal);
        }

        var blockTerminalComparer = new BlockTerminalComparer();

        foreach (var terminalTypeReferenceRequest in request.Terminals)
        {
            var blockTerminal = new BlockTerminalTypeReference
            {
                BlockId = id,
                TerminalId = terminalTypeReferenceRequest.TerminalId,
                Direction = terminalTypeReferenceRequest.Direction,
                MinCount = terminalTypeReferenceRequest.MinCount,
                MaxCount = terminalTypeReferenceRequest.MaxCount,
                ConnectionPointId = terminalTypeReferenceRequest.ConnectionPointId
            };

            if (block.Terminals.Contains(blockTerminal, blockTerminalComparer)) continue;

            var blockTerminalToUpdate = block.Terminals.FirstOrDefault(x => x.TerminalId == terminalTypeReferenceRequest.TerminalId && x.Direction == terminalTypeReferenceRequest.Direction);

            if (blockTerminalToUpdate != null)
            {
                blockTerminalToUpdate.MinCount = blockTerminal.MinCount;
                blockTerminalToUpdate.MaxCount = blockTerminal.MaxCount;
                blockTerminalToUpdate.ConnectionPointId = blockTerminal.ConnectionPointId;
            }
            else
            {
                block.Terminals.Add(blockTerminal);
            }
        }

        var blockAttributesToRemove = block.Attributes.Where(x => request.Attributes.All(y => y.AttributeId != x.AttributeId)).ToList();
        foreach (var blockAttribute in blockAttributesToRemove)
        {
            block.Attributes.Remove(blockAttribute);
        }

        var blockAttributeComparer = new BlockAttributeComparer();

        foreach (var attributeTypeReferenceRequest in request.Attributes)
        {
            var blockAttribute = new BlockAttributeTypeReference
            {
                BlockId = id,
                AttributeId = attributeTypeReferenceRequest.AttributeId,
                MinCount = attributeTypeReferenceRequest.MinCount,
                MaxCount = attributeTypeReferenceRequest.MaxCount
            };

            if (block.Attributes.Contains(blockAttribute, blockAttributeComparer)) continue;

            var blockAttributeToUpdate = block.Attributes.FirstOrDefault(x => x.AttributeId == attributeTypeReferenceRequest.AttributeId);

            if (blockAttributeToUpdate != null)
            {
                blockAttributeToUpdate.MinCount = blockAttribute.MinCount;
                blockAttributeToUpdate.MaxCount = blockAttribute.MaxCount;
            }
            else
            {
                block.Attributes.Add(blockAttribute);
            }
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var block = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (block == null)
        {
            return false;
        }

        if (block.State == State.Approved)
        {
            throw new InvalidOperationException("Approved blocks cannot be deleted.");
        }

        _dbSet.Remove(block);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeState(Guid id, State state)
    {
        var block = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (block == null)
        {
            return false;
        }

        if (state == State.Approved)
        {
            var completeBlock = await Get(id);

            if (completeBlock == null)
            {
                return false;
            }

            var blockAsJsonLd = await _jsonLdConversionService.ConvertToJsonLd(completeBlock);

            var postResponse = await _downstreamApi.CallApiForAppAsync("CommonLib", options =>
            {
                options.HttpMethod = "POST";
                options.RelativePath = "/api/imftype/WriteImfType";
                options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";

                options.CustomizeHttpRequestMessage = message =>
                {
                    message.Content = new StringContent(blockAsJsonLd.ToString(), System.Text.Encoding.UTF8, "application/json-patch+json");
                };
            });

            if (!postResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Post request to Common Library failed.");
            }
        }

        block.State = state;
        await _context.SaveChangesAsync();

        return true;
    }
}