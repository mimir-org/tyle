using System;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AspectObjectTerminalProfile : Profile
{
    public AspectObjectTerminalProfile()
    {
        CreateMap<AspectObjectTerminalLibAm, AspectObjectTerminalLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinQuantity == 0 ? 1 : src.MinQuantity))
            .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxQuantity == 0 ? int.MaxValue : src.MaxQuantity))
            .ForMember(dest => dest.ConnectorDirection, opt => opt.MapFrom(src => src.ConnectorDirection))
            .ForMember(dest => dest.AspectObjectId, opt => opt.Ignore())
            .ForMember(dest => dest.AspectObject, opt => opt.Ignore())
            .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalId))
            .ForMember(dest => dest.Terminal, opt => opt.Ignore());

        CreateMap<AspectObjectTerminalLibDm, AspectObjectTerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinQuantity))
            .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxQuantity))
            .ForMember(dest => dest.ConnectorDirection, opt => opt.MapFrom(src => src.ConnectorDirection))
            .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.Terminal));
    }
}