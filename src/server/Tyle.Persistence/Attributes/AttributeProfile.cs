using AutoMapper;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeProfile : Profile
{
    public AttributeProfile()
    {
        CreateMap<AttributeType, AttributeDao>()
            .ForMember(dest => dest.PredicateId, opt =>
            {
                opt.PreCondition(src => (src.Predicate != null));
                opt.MapFrom(src => src.Predicate!.Id);
            })
            .ForMember(dest => dest.Predicate, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.Units.Select(x => new AttributeUnitDao(src.Id, x.Id))))
            .ForMember(dest => dest.ProvenanceQualifier, opt =>
            {
                opt.PreCondition(src => (src.ProvenanceQualifier != null));
                opt.MapFrom(src => src.ProvenanceQualifier.ToString());
            })
            .ForMember(dest => dest.RangeQualifier, opt =>
            {
                opt.PreCondition(src => (src.RangeQualifier != null));
                opt.MapFrom(src => src.RangeQualifier.ToString());
            })
            .ForMember(dest => dest.RegularityQualifier, opt =>
            {
                opt.PreCondition(src => (src.RegularityQualifier != null));
                opt.MapFrom(src => src.RegularityQualifier.ToString());
            })
            .ForMember(dest => dest.ScopeQualifier, opt =>
            {
                opt.PreCondition(src => (src.ScopeQualifier != null));
                opt.MapFrom(src => src.ScopeQualifier.ToString());
            });
    }
}
