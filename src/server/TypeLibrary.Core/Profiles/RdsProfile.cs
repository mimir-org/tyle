using System.Web;
using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class RdsProfile : Profile
    {
        public RdsProfile(IApplicationSettingsRepository settings)
        {
            CreateMap<RdsLibAm, RdsLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.GetCurrentOntologyIri()}rds/{HttpUtility.UrlEncode(src.Code)}"))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToUriString()));

            CreateMap<RdsLibDm, RdsLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToArray()))
                .ForMember(dest => dest.Kind, opt => opt.Ignore());
        }
    }
}