using System;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class BlockTerminalProfile : Profile
{
    public BlockTerminalProfile()
    {
        CreateMap<BlockTerminalLibAm, BlockTerminalLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.MinCount, opt => opt.MapFrom(src => src.MinQuantity == 0 ? 1 : src.MinQuantity))
            .ForMember(dest => dest.MaxCount, opt => opt.MapFrom(src => src.MaxQuantity == 0 ? int.MaxValue : src.MaxQuantity))
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
            .ForMember(dest => dest.BlockId, opt => opt.Ignore())
            .ForMember(dest => dest.Block, opt => opt.Ignore())
            .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalId))
            .ForMember(dest => dest.Terminal, opt => opt.Ignore());

        CreateMap<BlockTerminalLibDm, BlockTerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinCount))
            .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxCount))
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
            .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.Terminal));
    }
}