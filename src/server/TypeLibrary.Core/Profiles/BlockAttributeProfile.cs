using System;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class BlockAttributeProfile : Profile
{
    public BlockAttributeProfile()
    {
        CreateMap<BlockAttributeLibAm, BlockAttributeTypeReference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MinCount, opt => opt.MapFrom(src => src.MinCount))
            .ForMember(dest => dest.MaxCount, opt => opt.MapFrom(src => src.MaxCount))
            .ForMember(dest => dest.Block, opt => opt.Ignore())
            .ForMember(dest => dest.Attribute, opt => opt.Ignore());

        CreateMap<BlockAttributeTypeReference, BlockAttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MinCount, opt => opt.MapFrom(src => src.MinCount))
            .ForMember(dest => dest.MaxCount, opt => opt.MapFrom(src => src.MaxCount))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}