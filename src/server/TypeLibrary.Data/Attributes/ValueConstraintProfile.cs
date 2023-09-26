using AutoMapper;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class ValueConstraintProfile : Profile
{
    public ValueConstraintProfile()
    {
        CreateMap<ValueConstraintRequest, ValueConstraint>()
            .ForMember(x => x.AttributeId, opt => opt.Ignore())
            .ForMember(x => x.Attribute, opt => opt.Ignore())
            .ForMember(dest => dest.ValueList, opt =>
            {
                opt.PreCondition(src => src.ValueList != null);
                opt.MapFrom(src => src.ValueList!.Select(x => new ValueListEntry { EntryValue = x }));
            });
    }
}