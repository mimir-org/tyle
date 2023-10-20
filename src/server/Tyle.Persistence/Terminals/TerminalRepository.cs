using Microsoft.EntityFrameworkCore;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Common;
using Tyle.Core.Terminals;
using Tyle.Persistence.Common;

namespace Tyle.Persistence.Terminals;

public class TerminalRepository : ITerminalRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<TerminalType> _dbSet;
    private readonly IUserInformationService _userInformationService;

    public TerminalRepository(TyleDbContext context, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Terminals;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<TerminalType>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<TerminalType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Classifiers).ThenInclude(x => x.Classifier)
            .Include(x => x.Purpose)
            .Include(x => x.Medium)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TerminalType> Create(TerminalTypeRequest request)
    {
        var terminal = new TerminalType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            State = State.Draft,
            Notation = request.Notation,
            Symbol = request.Symbol,
            Aspect = request.Aspect,
            Qualifier = request.Qualifier
        };

        terminal.LastUpdateOn = terminal.CreatedOn;

        foreach (var classifierId in request.ClassifierIds)
        {
            if (await _context.Classifiers.AsNoTracking().AnyAsync(x => x.Id == classifierId))
            {
                terminal.Classifiers.Add(new TerminalClassifierJoin
                {
                    TerminalId = terminal.Id,
                    ClassifierId = classifierId
                });
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "classifier", classifierId.ToString()));
            }
        }

        if (request.PurposeId == null || await _context.Purposes.AsNoTracking().AnyAsync(x => x.Id == request.PurposeId))
        {
            terminal.PurposeId = request.PurposeId;
        }
        else
        {
            throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "purpose", request.PurposeId.ToString()));
        }

        if (request.MediumId == null || await _context.Media.AsNoTracking().AnyAsync(x => x.Id == request.MediumId))
        {
            terminal.MediumId = request.MediumId;
        }
        else
        {
            throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "medium", request.MediumId.ToString()));
        }

        foreach (var attributeTypeReferenceRequest in request.Attributes)
        {
            if (await _context.Attributes.AsNoTracking().AnyAsync(x => x.Id == attributeTypeReferenceRequest.AttributeId))
            {
                terminal.Attributes.Add(new TerminalAttributeTypeReference
                {
                    TerminalId = terminal.Id,
                    AttributeId = attributeTypeReferenceRequest.AttributeId,
                    MinCount = attributeTypeReferenceRequest.MinCount,
                    MaxCount = attributeTypeReferenceRequest.MaxCount
                });
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "attribute", attributeTypeReferenceRequest.AttributeId.ToString()));
            }
        }

        _dbSet.Add(terminal);
        await _context.SaveChangesAsync();

        return await Get(terminal.Id);
    }

    public async Task<TerminalType?> Update(Guid id, TerminalTypeRequest request)
    {
        var terminal = await _dbSet.AsTracking()
           .Include(x => x.Classifiers)
           .Include(x => x.Attributes)
           .AsSplitQuery()
           .FirstOrDefaultAsync(x => x.Id == id);

        if (terminal == null)
        {
            return null;
        }

        terminal.Name = request.Name;
        terminal.Description = request.Description;
        if (_userInformationService.GetUserId() != terminal.CreatedBy)
        {
            terminal.ContributedBy.Add(_userInformationService.GetUserId());
        }
        terminal.LastUpdateOn = DateTimeOffset.Now;

        var terminalClassifiersToRemove = terminal.Classifiers.Where(x => !request.ClassifierIds.Contains(x.ClassifierId)).ToList();
        foreach (var terminalClassifier in terminalClassifiersToRemove)
        {
            terminal.Classifiers.Remove(terminalClassifier);
        }

        foreach (var classifierId in request.ClassifierIds)
        {
            if (terminal.Classifiers.Any(x => x.ClassifierId == classifierId)) continue;

            if (await _context.Classifiers.AsNoTracking().AnyAsync(x => x.Id == classifierId))
            {
                terminal.Classifiers.Add(new TerminalClassifierJoin
                {
                    TerminalId = id,
                    ClassifierId = classifierId
                });
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Update, "classifier", classifierId.ToString()));
            }
        }

        if (terminal.PurposeId != request.PurposeId)
        {
            if (request.PurposeId == null || await _context.Purposes.AsNoTracking().AnyAsync(x => x.Id == request.PurposeId))
            {
                terminal.PurposeId = request.PurposeId;
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Update, "purpose", request.PurposeId.ToString()));
            }
        }

        terminal.Notation = request.Notation;
        terminal.Symbol = request.Symbol;
        terminal.Aspect = request.Aspect;

        if (terminal.MediumId != request.MediumId)
        {
            if (request.MediumId == null || await _context.Media.AsNoTracking().AnyAsync(x => x.Id == request.MediumId))
            {
                terminal.MediumId = request.MediumId;
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Update, "medium", request.MediumId.ToString()));
            }
        }

        terminal.Qualifier = request.Qualifier;

        var terminalAttributesToRemove = terminal.Attributes.Where(x => request.Attributes.All(y => y.AttributeId != x.AttributeId)).ToList();
        foreach (var terminalAttribute in terminalAttributesToRemove)
        {
            terminal.Attributes.Remove(terminalAttribute);
        }

        var terminalAttributeComparer = new TerminalAttributeComparer();

        foreach (var attributeTypeReferenceRequest in request.Attributes)
        {
            var terminalAttribute = new TerminalAttributeTypeReference
            {
                TerminalId = id,
                AttributeId = attributeTypeReferenceRequest.AttributeId,
                MinCount = attributeTypeReferenceRequest.MinCount,
                MaxCount = attributeTypeReferenceRequest.MaxCount
            };

            if (terminal.Attributes.Contains(terminalAttribute, terminalAttributeComparer)) continue;

            if (!await _context.Attributes.AnyAsync(x => x.Id == attributeTypeReferenceRequest.AttributeId))
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Update, "attribute", attributeTypeReferenceRequest.ToString()));
            }

            var terminalAttributeToUpdate = terminal.Attributes.FirstOrDefault(x => x.AttributeId == attributeTypeReferenceRequest.AttributeId);

            if (terminalAttributeToUpdate != null)
            {
                terminalAttributeToUpdate.MinCount = terminalAttribute.MinCount;
                terminalAttributeToUpdate.MaxCount = terminalAttribute.MaxCount;
            }
            else
            {
                terminal.Attributes.Add(terminalAttribute);
            }
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var terminal = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (terminal == null)
        {
            return false;
        }

        _dbSet.Remove(terminal);
        await _context.SaveChangesAsync();

        return true;
    }
}