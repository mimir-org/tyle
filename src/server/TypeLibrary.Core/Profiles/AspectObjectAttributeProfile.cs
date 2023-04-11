using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AspectObjectAttributeProfile : Profile
{
    public AspectObjectAttributeProfile()
    {
        CreateMap<AspectObjectAttributeLibAm, AspectObjectAttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AspectObjectId, opt => opt.Ignore())
            .ForMember(dest => dest.AspectObject, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.AttributeId))
            .ForMember(dest => dest.Attribute, opt => opt.Ignore());

        CreateMap<AspectObjectAttributeLibDm, AspectObjectAttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}