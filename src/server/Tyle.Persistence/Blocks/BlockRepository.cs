using Microsoft.EntityFrameworkCore;
using Tyle.Application.Blocks;
using Tyle.Application.Blocks.Requests;
using Tyle.Application.Common;
using Tyle.Core.Blocks;
using Tyle.Core.Common;

namespace Tyle.Persistence.Blocks;

public class BlockRepository : IBlockRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<BlockType> _dbSet;
    private readonly IUserInformationService _userInformationService;

    public BlockRepository(TyleDbContext context, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Blocks;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<BlockType>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Medium)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<BlockType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Purpose)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Medium)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Terminals).ThenInclude(x => x.Terminal).ThenInclude(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ApiResponse<BlockType>> Create(BlockTypeRequest request)
    {

        var response = new ApiResponse<BlockType>();

        var block = new BlockType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            Notation = request.Notation,
            Symbol = request.Symbol,
            Aspect = request.Aspect
        };

        block.LastUpdateOn = block.CreatedOn;

        foreach (var classifierId in request.ClassifierIds)
        {
            if (await _context.Classifiers.AsNoTracking().AnyAsync(x => x.Id == classifierId))
            {
                block.Classifiers.Add(new BlockClassifierJoin
                {
                    BlockId = block.Id,
                    ClassifierId = classifierId
                });
            }
            else
            {
                response.ErrorMessage.Add("Could not add classifier. Please check the classifier-id and try again later.");
            }
        }

        if (request.PurposeId == null || await _context.Purposes.AsNoTracking().AnyAsync(x => x.Id == request.PurposeId))
        {
            block.PurposeId = request.PurposeId;
        }
        else
        {
            response.ErrorMessage.Add("Could not add purpose. Please check the purpose-id and try again later.");
        }

        foreach (var terminalTypeReferenceRequest in request.Terminals)
        {
            if (await _context.Terminals.AsNoTracking().AnyAsync(x => x.Id == terminalTypeReferenceRequest.TerminalId))
            {
                block.Terminals.Add(new BlockTerminalTypeReference
                {
                    BlockId = block.Id,
                    TerminalId = terminalTypeReferenceRequest.TerminalId,
                    Direction = terminalTypeReferenceRequest.Direction,
                    MinCount = terminalTypeReferenceRequest.MinCount,
                    MaxCount = terminalTypeReferenceRequest.MaxCount
                });
            }
            else
            {
                response.ErrorMessage.Add("Could not add terminal. Please check the terminal-id and try again later.");
            }
        }

        foreach (var attributeTypeReferenceRequest in request.Attributes)
        {
            if (await _context.Attributes.AsNoTracking().AnyAsync(x => x.Id == attributeTypeReferenceRequest.AttributeId))
            {
                block.Attributes.Add(new BlockAttributeTypeReference
                {
                    BlockId = block.Id,
                    AttributeId = attributeTypeReferenceRequest.AttributeId,
                    MinCount = attributeTypeReferenceRequest.MinCount,
                    MaxCount = attributeTypeReferenceRequest.MaxCount
                });
            }
            else
            {
                response.ErrorMessage.Add("Could not add attribute. Please check the attribute-id and try again later.");
            }
        }

        _dbSet.Add(block);
        await _context.SaveChangesAsync();

        response.TValue = await Get(block.Id);

        return response;
    }

    public async Task<ApiResponse<BlockType?>> Update(Guid id, BlockTypeRequest request)
    {
        var response = new ApiResponse<BlockType?>();

        var block = await _dbSet.AsTracking()
            .Include(x => x.Classifiers)
            .Include(x => x.Terminals)
            .Include(x => x.Terminals)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (block == null)
        {
            return null;
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

            if (await _context.Classifiers.AsNoTracking().AnyAsync(x => x.Id == classifierId))
            {
                block.Classifiers.Add(new BlockClassifierJoin
                {
                    BlockId = id,
                    ClassifierId = classifierId
                });
            }
            else
            {
                response.ErrorMessage.Add("Could not add classifier. Please check the id and try again later.");
            }
        }

        if (block.PurposeId != request.PurposeId)
        {
            if (request.PurposeId == null || await _context.Purposes.AsNoTracking().AnyAsync(x => x.Id == request.PurposeId))
            {
                block.PurposeId = request.PurposeId;
            }
            else
            {
                response.ErrorMessage.Add("Could not add purpose. Please check the id and try again later.");
            }
        }

        block.Notation = request.Notation;
        block.Symbol = request.Symbol;
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
                MaxCount = terminalTypeReferenceRequest.MaxCount
            };

            if (block.Terminals.Contains(blockTerminal, blockTerminalComparer)) continue;

            if (!await _context.Terminals.AnyAsync(x => x.Id == terminalTypeReferenceRequest.TerminalId))
            {
                response.ErrorMessage.Add("Could not add terminal. Please check the id and try again later.");
                continue;
            }

            var blockTerminalToUpdate = block.Terminals.FirstOrDefault(x => x.TerminalId == terminalTypeReferenceRequest.TerminalId && x.Direction == terminalTypeReferenceRequest.Direction);

            if (blockTerminalToUpdate != null)
            {
                blockTerminalToUpdate.MinCount = blockTerminal.MinCount;
                blockTerminalToUpdate.MaxCount = blockTerminal.MaxCount;
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

            if (!await _context.Attributes.AnyAsync(x => x.Id == attributeTypeReferenceRequest.AttributeId))
            {
                response.ErrorMessage.Add("Could not add attribute. Please check the id and try again later.");
                continue;
            }

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

        response.TValue = await Get(id);

        return response;
    }

    public async Task<bool> Delete(Guid id)
    {
        var block = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (block == null)
        {
            return false;
        }

        _dbSet.Remove(block);
        await _context.SaveChangesAsync();

        return true;
    }
}