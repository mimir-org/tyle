using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeType> _dbSet;
    private readonly IMapper _mapper;

    public AttributeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Attributes;
        _mapper = mapper;
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
            CreatedBy = "",
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
            // TODO: Handle the case where a request is sent with a non-valid predicate id
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
                // TODO: Handle the case where a request is sent with a non-valid unit id
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
            .Include(x => x.ValueConstraint)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return null;
        }

        attribute.Name = request.Name;
        attribute.Description = request.Description;
        // TODO: Update last updated and contributors

        if (request.PredicateId == null || await _context.Predicates.AsNoTracking().AnyAsync(x => x.Id == request.PredicateId))
        {
            attribute.PredicateId = request.PredicateId;
        }
        else
        {
            // TODO: Handle the case where a request is sent with a non-valid predicate id
        }

        var attributeUnitJoinsToRemove = attribute.Units.Where(x => !request.UnitIds.Contains(x.UnitId)).ToList();
        foreach (var attributeUnitJoin in attributeUnitJoinsToRemove)
        {
            attribute.Units.Remove(attributeUnitJoin);
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
                // TODO: Handle the case where a request is sent with a non-valid unit id
            }
        }

        attribute.UnitMinCount = request.UnitMinCount;
        attribute.UnitMaxCount = request.UnitMaxCount;

        attribute.ProvenanceQualifier = request.ProvenanceQualifier;
        attribute.RangeQualifier = request.RangeQualifier;
        attribute.RegularityQualifier = request.RegularityQualifier;
        attribute.ScopeQualifier = request.ScopeQualifier;

        if (attribute.ValueConstraint != null)
        {
            _context.ValueConstraints.Remove(attribute.ValueConstraint);
        }

        attribute.ValueConstraint = request.ValueConstraint == null ? null : _mapper.Map<ValueConstraint>(request.ValueConstraint);

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var attributeStub = new AttributeType { Id = id };
            _dbSet.Remove(attributeStub);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

        return true;
    }
}
