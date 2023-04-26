using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using TypeLibrary.Core.Factories;
using TypeLibrary.Core.Profiles.Resolvers;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class TerminalProfile : Profile
{
    public TerminalProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<TerminalLibAm, TerminalLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(new TerminalIriResolver(settings)))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.TerminalAspectObjects, opt => opt.Ignore())
            .ForMember(dest => dest.Attributes, opt => opt.Ignore())
            .ForMember(dest => dest.TerminalAttributes, opt => opt.Ignore());

        CreateMap<TerminalLibDm, TerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes));

        CreateMap<TerminalLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Terminal"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}