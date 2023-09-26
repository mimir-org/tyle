using AutoMapper;
using System.Globalization;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Api.Attributes;

public class AttributeTypeViewProfile : Profile
{
    public AttributeTypeViewProfile()
    {
        CreateMap<AttributeType, AttributeTypeView>()
            .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units.Select(x => x.Unit)))
            .ForMember(dest => dest.ValueConstraint, opt => opt.MapFrom(src => CreateValueConstraintView(src.ValueConstraint)));
    }

    private static ValueConstraintView CreateValueConstraintView(ValueConstraint? valueConstraint)
    {
        if (valueConstraint == null)
        {
            return null;
        }

        if (valueConstraint.ConstraintType == ConstraintType.HasValue)
        {
            if (valueConstraint.DataType is XsdDataType.Boolean)
            {
                var result = new HasBooleanValueConstraintView
                {
                    Value = bool.Parse(valueConstraint.Value!)
                };
                return MapCommonValueConstraintViewFields(result, valueConstraint);
            }
            if (valueConstraint.DataType is XsdDataType.Decimal or XsdDataType.Integer)
            {
                var result = new HasNumericValueConstraintView
                {
                    Value = decimal.Parse(valueConstraint.Value!, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                };
                return MapCommonValueConstraintViewFields(result, valueConstraint);
            }
            else
            {
                var result = new HasStringValueConstraintView
                {
                    Value = valueConstraint.Value!
                };
                return MapCommonValueConstraintViewFields(result, valueConstraint);
            }
        }

        else if (valueConstraint.ConstraintType == ConstraintType.In)
        {
            if (valueConstraint.DataType is XsdDataType.Decimal or XsdDataType.Integer)
            {
                var result = new NumericValueListConstraintView
                {
                    ValueList = valueConstraint.ValueList!.Select(x => decimal.Parse(x.EntryValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
                };
                return MapCommonValueConstraintViewFields(result, valueConstraint);
            }
            else
            {
                var result = new StringValueListConstraintView
                {
                    ValueList = valueConstraint.ValueList!.Select(x => x.EntryValue)
                };
                return MapCommonValueConstraintViewFields(result, valueConstraint);
            }
        }

        else
        {
            var result = new ValueConstraintView();
            return MapCommonValueConstraintViewFields(result, valueConstraint);
        }
    }

    private static ValueConstraintView MapCommonValueConstraintViewFields(ValueConstraintView result, ValueConstraint valueConstraint)
    {
        result.ConstraintType = valueConstraint.ConstraintType;
        result.DataType = valueConstraint.DataType;
        result.MinCount = valueConstraint.MinCount;
        result.MaxCount = valueConstraint.MaxCount;
        result.Pattern = valueConstraint.Pattern;
        result.MinValue = valueConstraint.MinValue;
        result.MaxValue = valueConstraint.MaxValue;
        result.MinInclusive = valueConstraint.MinInclusive;
        result.MaxInclusive = valueConstraint.MaxInclusive;

        return result;
    }
}