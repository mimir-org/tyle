using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class QuantityDatumProfile : Profile
{
    public QuantityDatumProfile()
    {
        CreateMap<QuantityDatumDm, QuantityDatumCm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.QuantityDatumType, opt => opt.MapFrom(src => src.QuantityDatumType));
    }
}