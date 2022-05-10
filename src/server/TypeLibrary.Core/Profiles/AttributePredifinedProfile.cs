using System.Web;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class AttributePredefinedProfile : Profile
    {
        public AttributePredefinedProfile(IApplicationSettingsRepository settings)
        {
            CreateMap<AttributePredefinedLibAm, AttributePredefinedLibDm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.Trim()))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/attribute/predefined/{HttpUtility.UrlEncode(src.Key.Trim())}"))
                .ForMember(dest => dest.ValueStringList, opt => opt.MapFrom(src => src.ValueStringList))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect));

            CreateMap<AttributePredefinedLibDm, AttributePredefinedLibCm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.ValueStringList, opt => opt.MapFrom(src => src.ValueStringList))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect));
        }
    }
}