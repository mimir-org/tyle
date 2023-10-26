using AutoMapper;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;

namespace Tyle.Persistence.Common;

public class ClassifierProfile : Profile
{
    public ClassifierProfile()
    {
        CreateMap<RdlClassifierRequest, RdlClassifier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => new Uri(src.Iri)))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => ReferenceSource.UserSubmission));
    }
}