using System;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Core.Profiles;

public class TerminalAttributeProfile : Profile
{
    public TerminalAttributeProfile()
    {
        CreateMap<TerminalAttributeTypeReference, AttributeTypeReferenceView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MinCount, opt => opt.MapFrom(src => src.MinCount))
            .ForMember(dest => dest.MaxCount, opt => opt.MapFrom(src => src.MaxCount))
            .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
    }
}