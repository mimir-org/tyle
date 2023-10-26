using AutoMapper;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Persistence.Terminals;

public class MediumProfile : Profile
{
    public MediumProfile()
    {
        CreateMap<RdlMediumRequest, RdlMedium>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => ReferenceSource.UserSubmission));
    }
}