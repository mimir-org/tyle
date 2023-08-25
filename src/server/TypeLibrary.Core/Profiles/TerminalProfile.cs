using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Linq;
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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdateOn, opt => opt.Ignore())
            //.ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
            .ForMember(dest => dest.Notation, opt => opt.MapFrom(src => src.Notation))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium))
            .ForMember(dest => dest.Qualifier, opt => opt.MapFrom(src => src.Qualifier))
            .ForMember(dest => dest.TerminalBlocks, opt => opt.Ignore())
            .ForMember(dest => dest.TerminalAttributes, opt => opt.MapFrom(src => src.TerminalAttributes));

        CreateMap<TerminalLibDm, TerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.ContributedBy, opt => opt.MapFrom(src => src.ContributedBy))
            .ForMember(dest => dest.LastUpdateOn, opt => opt.MapFrom(src => src.LastUpdateOn))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
            .ForMember(dest => dest.Notation, opt => opt.MapFrom(src => src.Notation))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium))
            .ForMember(dest => dest.Qualifier, opt => opt.MapFrom(src => src.Qualifier))
            .ForMember(dest => dest.TerminalAttributes, opt => opt.MapFrom(src => src.TerminalAttributes));

        CreateMap<TerminalLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            //.ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Terminal"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}