using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitLibAm, UnitLibDm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));


        CreateMap<UnitLibDm, UnitLibCm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));
    }
}