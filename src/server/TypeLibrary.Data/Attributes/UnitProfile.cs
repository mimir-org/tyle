using AutoMapper;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitReference, UnitDao>()
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri.AbsoluteUri))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()));

        CreateMap<UnitDao, UnitReference>()
            .ConstructUsing(src => new UnitReference(
                src.Name,
                new Uri(src.Iri),
                src.Symbol,
                src.Description,
                Enum.Parse<ReferenceSource>(src.Source)));
    }
}