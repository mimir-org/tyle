using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Core.Profiles;

public class ClassifierProfile : Profile
{
    public ClassifierProfile()
    {
        CreateMap<ClassifierReferenceAm, ClassifierReference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.Ignore())
            .ForMember(dest => dest.Blocks, opt => opt.Ignore())
            .ForMember(dest => dest.Terminals, opt => opt.Ignore());

        CreateMap<ClassifierReference, ClassifierReferenceCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source));
    }
}