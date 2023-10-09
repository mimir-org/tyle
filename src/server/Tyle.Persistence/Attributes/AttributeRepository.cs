using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

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

    public async Task<ApiResponse<AttributeType>> Create(AttributeTypeRequest request)
    {
        var response = new ApiResponse<AttributeType>();
        response.TValue = new AttributeType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            UnitMinCount = request.UnitMinCount,
            UnitMaxCount = request.UnitMaxCount,
            ProvenanceQualifier = request.ProvenanceQualifier,
            RangeQualifier = request.RangeQualifier,
            RegularityQualifier = request.RegularityQualifier,
            ScopeQualifier = request.ScopeQualifier,
            ValueConstraint = _mapper.Map<ValueConstraint>(request.ValueConstraint)
        };

        response.TValue.LastUpdateOn = response.TValue.CreatedOn;

        if (request.PredicateId == null || await _context.Predicates.AsNoTracking().AnyAsync(x => x.Id == request.PredicateId))
        {
            response.TValue.PredicateId = request.PredicateId;
        }
        else
        {
            response.ErrorMessage.Add($"Could not add predicate. Please check and try again later");
        }

        foreach (var unitId in request.UnitIds)
        {
            if (await _context.Units.AsNoTracking().AnyAsync(x => x.Id == unitId))
            {
                response.TValue.Units.Add(new AttributeUnitJoin
                {
                    AttributeId = response.TValue.Id,
                    UnitId = unitId
                });
            }
            else
            {
                response.ErrorMessage.Add($"Adding the attribute {request.Name} failed. Please check your input");
            }
        }

        _dbSet.Add(response.TValue);
        await _context.SaveChangesAsync();

        response.TValue = await Get(response.TValue.Id);
        return response;
    }

    public async Task<ApiResponse<AttributeType?>> Update(Guid id, AttributeTypeRequest request)
    {
        var response = new ApiResponse<AttributeType?>();

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
                response.ErrorMessage.Add($"Could not add predicate. Please ensure the predicate is correct and try again later.");
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
                response.ErrorMessage.Add("could not remove one or more of the attributes. Please ensure the attribute to remove is correct and try again later.");
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

        response.TValue = await Get(id);

        return response;
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
}