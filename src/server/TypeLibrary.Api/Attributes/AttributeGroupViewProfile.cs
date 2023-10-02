using AutoMapper;
using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Api.Attributes;

public class AttributeGroupViewProfile : Profile
{
    public AttributeGroupViewProfile()
    {
        CreateMap<AttributeGroup, AttributeGroupView>()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.Select(x => x.Attribute)));
    }
}
