using System;
using System.Linq;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeGroupProfile : Profile
{
    public AttributeGroupProfile()
    {
        CreateMap<AttributeGroupLibAm, AttributeGroupLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Attributes, opt => opt.Ignore());

        CreateMap<AttributeGroupLibDm, AttributeGroupLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.Select(x => x.Attribute)));
    }
}