using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class NodeTerminalProfile : Profile
{
    public NodeTerminalProfile()
    {
        CreateMap<NodeTerminalLibDm, NodeTerminalLibAm>()
            .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalId))
            .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinQuantity == 0 ? 1 : src.MinQuantity))
            .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxQuantity == 0 ? int.MaxValue : src.MaxQuantity))
            .ForMember(dest => dest.ConnectorDirection, opt => opt.MapFrom(src => src.ConnectorDirection));

        CreateMap<NodeTerminalLibDm, NodeTerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinQuantity))
            .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxQuantity))
            .ForMember(dest => dest.ConnectorDirection, opt => opt.MapFrom(src => src.ConnectorDirection))
            .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.Terminal));
    }
}