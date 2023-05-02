using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class PurposeProfile : Profile
{
    public PurposeProfile()
    {
        CreateMap<PurposeLibDm, PurposeLibCm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source));
    }
}