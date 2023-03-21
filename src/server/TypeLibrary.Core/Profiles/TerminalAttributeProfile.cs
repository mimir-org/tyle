using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class TerminalAttributeProfile : Profile
{
    public TerminalAttributeProfile()
    {
        CreateMap<TerminalAttributeLibDm, TerminalAttributeLibAm>()
            .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.AttributeId));

        CreateMap<TerminalAttributeLibDm, TerminalAttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}