using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class PredicateProfile : Profile
{
    public PredicateProfile()
    {
        CreateMap<PredicateReferenceAm, PredicateReference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.Ignore());

        CreateMap<PredicateReference, PredicateReferenceCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source));
    }
}