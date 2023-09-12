using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Core.Profiles;

public class SymbolProfile : Profile
{
    public SymbolProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, IOptions<ApplicationSettings> applicationSettings)
    {
        CreateMap<SymbolLibAm, SymbolLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.Ignore())
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

        CreateMap<SymbolLibDm, SymbolLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Data) ? null : $"{applicationSettings.Value.ApplicationUrl}/symbol/{src.Id}.svg"));
    }
}