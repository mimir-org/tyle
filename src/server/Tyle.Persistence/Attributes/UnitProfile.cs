using AutoMapper;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<RdlUnitRequest, RdlUnit>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => ReferenceSource.UserSubmission));
    }
}