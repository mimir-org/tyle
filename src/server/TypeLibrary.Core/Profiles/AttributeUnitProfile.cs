using System;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeUnitProfile : Profile
{
    public AttributeUnitProfile()
    {
        CreateMap<AttributeUnitLibAm, AttributeUnitLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault))
            .ForMember(dest => dest.AttributeId, opt => opt.Ignore())
            .ForMember(dest => dest.Attribute, opt => opt.Ignore())
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
            .ForMember(dest => dest.Unit, opt => opt.Ignore());

        CreateMap<AttributeUnitLibDm, AttributeUnitLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));
    }
}