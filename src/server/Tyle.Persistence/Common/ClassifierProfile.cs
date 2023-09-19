using AutoMapper;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class ClassifierProfile : Profile
{
    public ClassifierProfile()
    {
        CreateMap<ClassifierReference, ClassifierDao>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri.AbsoluteUri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()));

        CreateMap<ClassifierDao, ClassifierReference>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => Enum.Parse<ReferenceSource>(src.Source)));
    }
}
