using System.Globalization;
using AutoMapper;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Attributes.ValueConstraints;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class AttributeProfile : Profile
{
    public AttributeProfile()
    {
        CreateMap<AttributeType, AttributeDao>()
            .ForMember(dest => dest.PredicateId, opt =>
            {
                opt.PreCondition(src => (src.Predicate != null));
                opt.MapFrom(src => src.Predicate!.Id);
            })
            .ForMember(dest => dest.Predicate, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.Units.Select(x => new AttributeUnitDao(src.Id, x.Id))))
            .ForMember(dest => dest.ProvenanceQualifier, opt =>
            {
                opt.PreCondition(src => (src.ProvenanceQualifier != null));
                opt.MapFrom(src => src.ProvenanceQualifier.ToString());
            })
            .ForMember(dest => dest.RangeQualifier, opt =>
            {
                opt.PreCondition(src => (src.RangeQualifier != null));
                opt.MapFrom(src => src.RangeQualifier.ToString());
            })
            .ForMember(dest => dest.RegularityQualifier, opt =>
            {
                opt.PreCondition(src => (src.RegularityQualifier != null));
                opt.MapFrom(src => src.RegularityQualifier.ToString());
            })
            .ForMember(dest => dest.ScopeQualifier, opt =>
            {
                opt.PreCondition(src => (src.ScopeQualifier != null));
                opt.MapFrom(src => src.ScopeQualifier.ToString());
            });

        CreateMap<AttributeDao, AttributeType>()
            .ConstructUsing(src => new AttributeType(src.Name, src.Description, new User("", "")))
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.AttributeUnits.Select(x => x.Unit)))
            .ForMember(dest => dest.ProvenanceQualifier, opt =>
            {
                opt.PreCondition(src => (src.ProvenanceQualifier != null));
                opt.MapFrom(src => Enum.Parse<ProvenanceQualifier>(src.ProvenanceQualifier!));
            })
            .ForMember(dest => dest.RangeQualifier, opt =>
            {
                opt.PreCondition(src => (src.RangeQualifier != null));
                opt.MapFrom(src => Enum.Parse<RangeQualifier>(src.RangeQualifier!));
            })
            .ForMember(dest => dest.RegularityQualifier, opt =>
            {
                opt.PreCondition(src => (src.RegularityQualifier != null));
                opt.MapFrom(src => Enum.Parse<RegularityQualifier>(src.RegularityQualifier!));
            })
            .ForMember(dest => dest.ScopeQualifier, opt =>
            {
                opt.PreCondition(src => (src.ScopeQualifier != null));
                opt.MapFrom(src => Enum.Parse<ScopeQualifier>(src.ScopeQualifier!));
            })
            .ForMember(dest => dest.ValueConstraint, opt => opt.MapFrom(src => MapValueConstraint(src.ValueConstraint)));
    }

    private IValueConstraint MapValueConstraint(ValueConstraintDao? valueConstraintDao)
    {
        if (valueConstraintDao == null)
        {
            return null;
        }

        var constraintType = Enum.Parse<ConstraintType>(valueConstraintDao.ConstraintType);
        var dataType = Enum.Parse<XsdDataType>(valueConstraintDao.DataType);
        var valueList = valueConstraintDao.ValueList.Select(x => x.ValueListEntry).ToList();

        switch (constraintType)
        {
            case ConstraintType.HasValue:
                switch (dataType)
                {
                    case XsdDataType.String:
                        return new HasStringValue(valueConstraintDao.Value!);
                    case XsdDataType.Decimal:
                        return new HasDecimalValue(decimal.Parse(valueConstraintDao.Value!, CultureInfo.InvariantCulture));
                    case XsdDataType.Integer:
                        return new HasIntegerValue(int.Parse(valueConstraintDao.Value!));
                    case XsdDataType.AnyUri:
                        return new HasIriValue(new Uri(valueConstraintDao.Value!));
                }
                break;
            case ConstraintType.In:
                switch (dataType)
                {
                    case XsdDataType.String:
                        return new InStringValueList(valueList, (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Decimal:
                        return new InDecimalValueList(valueList.Select(x => decimal.Parse(x, CultureInfo.InvariantCulture)).ToList(), (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Integer:
                        return new InIntegerValueList(valueList.Select(int.Parse).ToList(), (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.AnyUri:
                        return new InIriValueList(valueList.Select(x => new Uri(x)).ToList(), (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                }
                break;
            case ConstraintType.DataType:
                switch (dataType)
                {
                    case XsdDataType.String:
                        return new DataTypeString((int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Decimal:
                        return new DataTypeDecimal((int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Integer:
                        return new DataTypeInteger((int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Boolean:
                        return new DataTypeBoolean((int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                }
                break;
            case ConstraintType.Pattern:
                return new StringPattern(valueConstraintDao.Pattern!, (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
            case ConstraintType.Range:
                switch (dataType)
                {
                    case XsdDataType.Decimal:
                        return new RangeDecimal(valueConstraintDao.MinValue, valueConstraintDao.MaxValue, valueConstraintDao.MinInclusive, valueConstraintDao.MaxInclusive, (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                    case XsdDataType.Integer:
                        return new RangeInteger((int?)valueConstraintDao.MinValue, (int?)valueConstraintDao.MaxValue, valueConstraintDao.MinInclusive, valueConstraintDao.MaxInclusive, (int)valueConstraintDao.MinCount!, valueConstraintDao.MaxCount);
                }
                break;
        }

        return null;
    }
}
