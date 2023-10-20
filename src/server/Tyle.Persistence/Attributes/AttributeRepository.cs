using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Persistence.Common;

namespace Tyle.Persistence.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeType> _dbSet;
    private readonly IMapper _mapper;
    private readonly IUserInformationService _userInformationService;

    public AttributeRepository(TyleDbContext context, IMapper mapper, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Attributes;
        _mapper = mapper;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<AttributeType>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AttributeType> Create(AttributeTypeRequest request)
    {
        var attribute = new AttributeType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            State = State.Draft,
            UnitMinCount = request.UnitMinCount,
            UnitMaxCount = request.UnitMaxCount,
            ProvenanceQualifier = request.ProvenanceQualifier,
            RangeQualifier = request.RangeQualifier,
            RegularityQualifier = request.RegularityQualifier,
            ScopeQualifier = request.ScopeQualifier,
            ValueConstraint = _mapper.Map<ValueConstraint>(request.ValueConstraint)
        };

        attribute.LastUpdateOn = attribute.CreatedOn;

        if (request.PredicateId == null || await _context.Predicates.AsNoTracking().AnyAsync(x => x.Id == request.PredicateId))
        {
            attribute.PredicateId = request.PredicateId;
        }
        else
        {
            throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "predicate", request.PredicateId.ToString()));
        }

        foreach (var unitId in request.UnitIds)
        {
            if (await _context.Units.AsNoTracking().AnyAsync(x => x.Id == unitId))
            {
                attribute.Units.Add(new AttributeUnitJoin
                {
                    AttributeId = attribute.Id,
                    UnitId = unitId
                });
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "unit", unitId.ToString()));
            }
        }

        _dbSet.Add(attribute);
        await _context.SaveChangesAsync();

        return await Get(attribute.Id);
    }

    public async Task<AttributeType?> Update(Guid id, AttributeTypeRequest request)
    {
        var attribute = await _dbSet.AsTracking()
            .Include(x => x.Units)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return null;
        }

        attribute.Name = request.Name;
        attribute.Description = request.Description;
        if (_userInformationService.GetUserId() != attribute.CreatedBy)
        {
            attribute.ContributedBy.Add(_userInformationService.GetUserId());
        }
        attribute.LastUpdateOn = DateTimeOffset.Now;

        if (attribute.PredicateId != request.PredicateId)
        {
            if (request.PredicateId == null || await _context.Predicates.AsNoTracking().AnyAsync(x => x.Id == request.PredicateId))
            {
                attribute.PredicateId = request.PredicateId;
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "predicate", request.PredicateId.ToString()));
            }
        }

        var attributeUnitsToRemove = attribute.Units.Where(x => !request.UnitIds.Contains(x.UnitId)).ToList();
        foreach (var attributeUnit in attributeUnitsToRemove)
        {
            attribute.Units.Remove(attributeUnit);
        }

        foreach (var unitId in request.UnitIds)
        {
            if (attribute.Units.Any(x => x.UnitId == unitId)) continue;

            if (await _context.Units.AsNoTracking().AnyAsync(x => x.Id == unitId))
            {
                attribute.Units.Add(new AttributeUnitJoin
                {
                    AttributeId = id,
                    UnitId = unitId
                });
            }
            else
            {
                throw new KeyNotFoundException(ExceptionMessage.CreateExceptionMessage(ExceptionMessage.TypeOfMessage.Add, "unit", unitId.ToString()));
            }
        }

        attribute.UnitMinCount = request.UnitMinCount;
        attribute.UnitMaxCount = request.UnitMaxCount;

        attribute.ProvenanceQualifier = request.ProvenanceQualifier;
        attribute.RangeQualifier = request.RangeQualifier;
        attribute.RegularityQualifier = request.RegularityQualifier;
        attribute.ScopeQualifier = request.ScopeQualifier;

        var valueConstraintComparer = new ValueConstraintComparer();
        var requestedValueConstraint = _mapper.Map<ValueConstraint>(request.ValueConstraint);

        if (!valueConstraintComparer.Equals(attribute.ValueConstraint, requestedValueConstraint))
        {
            if (attribute.ValueConstraint != null)
            {
                if (attribute.ValueConstraint.ConstraintType == ConstraintType.In)
                {
                    _context.ValueListEntries.RemoveRange(attribute.ValueConstraint.ValueList);
                }
                _context.ValueConstraints.Remove(attribute.ValueConstraint);
            }

            attribute.ValueConstraint = requestedValueConstraint;
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var attribute = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return false;
        }

        _dbSet.Remove(attribute);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeState(Guid id, State state)
    {
        var attribute = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return false;
        }

        attribute.State = state;
        await _context.SaveChangesAsync();

        return true;
    }
}