using System.Web;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class SelectedAttributePredefinedProfile : Profile
    {
        public SelectedAttributePredefinedProfile(IApplicationSettingsRepository settings)
        {
            CreateMap<SelectedAttributePredefinedLibAm, SelectedAttributePredefinedLibDm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.Trim()))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/attribute/predefined/{HttpUtility.UrlEncode(src.Key)}"))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect));

            CreateMap<SelectedAttributePredefinedLibDm, SelectedAttributePredefinedLibCm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect));
        }
    }
}