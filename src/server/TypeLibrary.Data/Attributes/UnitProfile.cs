using AutoMapper;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Data.Attributes;

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