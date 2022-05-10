using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class SymbolProfile : Profile
    {
        public SymbolProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
        {
            CreateMap<SymbolLibAm, SymbolLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/symbol/{src.Id}"))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<SymbolLibDm, SymbolLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Data) ? null : $"{contextAccessor.GetApplicationUrl()}/symbol/{src.Id}.svg"));
        }
    }
}