using AutoMapper;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class PurposeProfile : Profile
{
    public PurposeProfile()
    {
        CreateMap<RdlPurposeRequest, RdlPurpose>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => ReferenceSource.UserSubmission));
    }
}