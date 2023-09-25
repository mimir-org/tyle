using AutoMapper;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Data.Common;

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
