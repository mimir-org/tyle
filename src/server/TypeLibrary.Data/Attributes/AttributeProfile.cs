using AutoMapper;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

public class AttributeProfile : Profile
{
    public AttributeProfile()
    {
        CreateMap<AttributeTypeRequest, AttributeType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Version, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdateOn, opt => opt.Ignore())
            .ForMember(dest => dest.Predicate, opt => opt.Ignore())
            .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.UnitIds.Select(x => new AttributeUnitJoin { UnitId = x })));
    }
}