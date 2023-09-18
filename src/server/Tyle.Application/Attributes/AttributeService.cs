using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;
using Tyle.Core.Attributes.ValueConstraints;

namespace Tyle.Application.Attributes;

public class AttributeService : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository;
    private readonly IReferenceService _referenceService;
    private readonly IUserService _userService;

    public AttributeService(IAttributeRepository attributeRepository, IReferenceService referenceService, IUserService userService)
    {
        _attributeRepository = attributeRepository;
        _referenceService = referenceService;
        _userService = userService;
    }

    public async Task<IEnumerable<AttributeType>> GetAll()
    {
        return await _attributeRepository.GetAll();
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        return await _attributeRepository.Get(id);
    }

    public async Task<AttributeType> Create(AttributeTypeRequest request)
    {
        var attribute = new AttributeType(request.Name, request.Description, await _userService.GetCurrentUser());

        await UpdateAttributeTypeFields(attribute, request);

        return await _attributeRepository.Create(attribute);
    }

    public async Task<AttributeType> Update(Guid id, AttributeTypeRequest request)
    {
        var attribute = await _attributeRepository.Get(id) ?? throw new KeyNotFoundException($"No attribute type with id {id} found.");

        attribute.Name = request.Name;
        attribute.Description = request.Description;
        
        var updatingUser = await _userService.GetCurrentUser();
        if (updatingUser != attribute.CreatedBy)
        {
            attribute.ContributedBy.Add(updatingUser);
        }

        attribute.LastUpdateOn = DateTimeOffset.Now;

        await UpdateAttributeTypeFields(attribute, request);

        return await _attributeRepository.Update(attribute);
    }

    public Task Delete(Guid id)
    {
        return _attributeRepository.Delete(id);
    }

    private async Task UpdateAttributeTypeFields(AttributeType attribute, AttributeTypeRequest request)
    {
        if (request.PredicateReferenceId == null)
        {
            attribute.Predicate = null;
        }
        else
        {
            var predicate = await _referenceService.GetPredicate((int)request.PredicateReferenceId) ?? throw new ArgumentException($"No predicate with id {request.PredicateReferenceId} found.", nameof(request));
            attribute.Predicate = predicate;
        }

        var unitsToRemove = attribute.Units.Where(unit => !request.UnitReferenceIds.Contains(unit.Id));
        foreach (var unit in unitsToRemove)
        {
            attribute.Units.Remove(unit);
        }

        foreach (var unitReferenceId in request.UnitReferenceIds)
        {
            if (attribute.Units.Select(x => x.Id).Contains(unitReferenceId)) continue;

            var unit = await _referenceService.GetUnit(unitReferenceId) ?? throw new ArgumentException($"No unit with id {unitReferenceId} found.", nameof(request));
            attribute.Units.Add(unit);
        }

        attribute.UnitMinCount = request.UnitMinCount;
        attribute.UnitMaxCount = request.UnitMaxCount;
        attribute.ProvenanceQualifier = request.ProvenanceQualifier;
        attribute.RangeQualifier = request.RangeQualifier;
        attribute.RegularityQualifier = request.RegularityQualifier;
        attribute.ScopeQualifier = request.ScopeQualifier;

        if (request.ValueConstraint == null)
        {
            attribute.ValueConstraint = null;
            return;
        }

        attribute.ValueConstraint = CreateValueConstraint(request.ValueConstraint);
    }

    private static IValueConstraint? CreateValueConstraint(ValueConstraintRequest request)
    {
        switch (request.ConstraintType)
        {
            case ConstraintType.HasValue:
                switch (request.DataType)
                {
                    case XsdDataType.String:
                        return new HasStringValue(request.Value!);
                    case XsdDataType.Decimal:
                        return new HasDecimalValue(decimal.Parse(request.Value!));
                    case XsdDataType.Integer:
                        return new HasIntegerValue(int.Parse(request.Value!));
                    case XsdDataType.AnyUri:
                        return new HasIriValue(new Uri(request.Value!));
                }
                break;
            case ConstraintType.In:
                switch (request.DataType)
                {
                    case XsdDataType.String:
                        return new InStringValueList(request.ValueList!, (int)request.MinCount!, request.MaxCount);
                    case XsdDataType.Decimal:
                        return new InDecimalValueList(request.ValueList!.Select(decimal.Parse).ToList(), (int)request.MinCount!, request.MaxCount);
                    case XsdDataType.Integer:
                        return new InIntegerValueList(request.ValueList!.Select(int.Parse).ToList(), (int)request.MinCount!, request.MaxCount);
                    case XsdDataType.AnyUri:
                        return new InIriValueList(request.ValueList!.Select(x => new Uri(x)).ToList(), (int) request.MinCount!, request.MaxCount);
                }
                break;
            case ConstraintType.DataType:
                switch (request.DataType)
                {
                    case XsdDataType.String:
                        return new DataTypeString((int)request.MinCount!, request.MaxCount);
                    case XsdDataType.Decimal:
                        return new DataTypeDecimal((int) request.MinCount!, request.MaxCount);
                    case XsdDataType.Integer:
                        return new DataTypeInteger((int) request.MinCount!, request.MaxCount);
                    case XsdDataType.Boolean:
                        return new DataTypeBoolean((int) request.MinCount!, request.MaxCount);
                }
                break;
            case ConstraintType.Pattern:
                return new StringPattern(request.Pattern!, (int) request.MinCount!, request.MaxCount);
            case ConstraintType.Range:
                if (request.DataType == XsdDataType.Decimal)
                {
                    return new RangeDecimal(request.MinValue, request.MaxValue, request.MinInclusive, request.MaxInclusive, (int) request.MinCount!, request.MaxCount);
                }
                else if (request.DataType == XsdDataType.Integer)
                {
                    return new RangeInteger((int?)request.MinValue, (int?)request.MaxValue, request.MinInclusive, request.MaxInclusive, (int) request.MinCount!, request.MaxCount);
                }
                break;
        }

        return null;
    }
}
