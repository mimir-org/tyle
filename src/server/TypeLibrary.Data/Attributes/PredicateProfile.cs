using AutoMapper;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class PredicateProfile : Profile
{
    public PredicateProfile()
    {
        CreateMap<PredicateReference, PredicateDao>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri.AbsoluteUri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()));

        CreateMap<PredicateDao, PredicateReference>()
            .ConstructUsing(src => new PredicateReference(
                src.Name,
                new Uri(src.Iri),
                src.Description,
                Enum.Parse<ReferenceSource>(src.Source)));
    }
}