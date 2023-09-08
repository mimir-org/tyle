using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitReferenceAm, UnitReference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.Ignore());

        CreateMap<UnitReference, UnitReferenceCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source));
    }
}