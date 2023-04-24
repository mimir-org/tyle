using System;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Constants;

namespace TypeLibrary.Core.Profiles;

public class AttributePredefinedProfile : Profile
{
    public AttributePredefinedProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<AttributePredefinedLibAm, AttributePredefinedLibDm>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.Trim()))
            .ForMember(dest => dest.Iri,
                opt => opt.MapFrom(src =>
                    $"{settings.ApplicationSemanticUrl}/attribute/predefined/{HttpUtility.UrlEncode(src.Key.Trim())}"))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy,
                opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedByConstants.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect))
            .ForMember(dest => dest.ValueStringList, opt => opt.MapFrom(src => src.ValueStringList));

        CreateMap<AttributePredefinedLibDm, AttributePredefinedLibCm>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect))
            .ForMember(dest => dest.ValueStringList, opt => opt.MapFrom(src => src.ValueStringList));
    }
}