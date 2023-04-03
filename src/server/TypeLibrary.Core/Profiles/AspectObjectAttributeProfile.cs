using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class NodeAttributeProfile : Profile
{
    public NodeAttributeProfile()
    {
        CreateMap<NodeAttributeLibAm, AspectObjectAttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NodeId, opt => opt.Ignore())
            .ForMember(dest => dest.AspectObject, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.AttributeId))
            .ForMember(dest => dest.Attribute, opt => opt.Ignore());

        CreateMap<AspectObjectAttributeLibDm, NodeAttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}