using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class TerminalAttributeProfile : Profile
{
    public TerminalAttributeProfile()
    {
        CreateMap<TerminalAttributeLibAm, TerminalAttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TerminalId, opt => opt.Ignore())
            .ForMember(dest => dest.Terminal, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.AttributeId))
            .ForMember(dest => dest.Attribute, opt => opt.Ignore());

        CreateMap<TerminalAttributeLibDm, TerminalAttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}