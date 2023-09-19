using AutoMapper;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class MediumProfile : Profile
{
    public MediumProfile()
    {
        CreateMap<MediumReference, MediumDao>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri.AbsoluteUri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()));

        CreateMap<MediumDao, MediumReference>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => Enum.Parse<ReferenceSource>(src.Source)));
    }
}