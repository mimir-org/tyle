using AutoMapper;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class PurposeProfile : Profile
{
    public PurposeProfile()
    {
        CreateMap<PurposeReference, PurposeDao>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri.AbsoluteUri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()));

        CreateMap<PurposeDao, PurposeReference>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => Enum.Parse<ReferenceSource>(src.Source)));
    }
}