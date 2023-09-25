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
            .ConstructUsing(src => new PurposeReference(
                src.Name,
                new Uri(src.Iri),
                src.Description,
                Enum.Parse<ReferenceSource>(src.Source)));
    }
}