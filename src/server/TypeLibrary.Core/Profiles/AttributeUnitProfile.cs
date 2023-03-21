using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeUnitProfile : Profile
{
    public AttributeUnitProfile()
    {
        CreateMap<AttributeUnitLibAm, AttributeUnitLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeId, opt => opt.Ignore())
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));

        CreateMap<AttributeUnitLibDm, AttributeUnitLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));
    }
}