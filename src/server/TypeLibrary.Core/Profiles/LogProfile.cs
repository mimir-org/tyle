using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class LogProfile : Profile
{
    public LogProfile(IHttpContextAccessor contextAccessor)
    {

        CreateMap<LogLibAm, LogLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectId))
            .ForMember(dest => dest.ObjectName, opt => opt.MapFrom(src => src.ObjectName))
            .ForMember(dest => dest.ObjectVersion, opt => opt.MapFrom(src => src.ObjectVersion))
            .ForMember(dest => dest.ObjectFirstVersionId, opt => opt.MapFrom(src => src.ObjectFirstVersionId))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => src.ObjectType))
            .ForMember(dest => dest.LogType, opt => opt.MapFrom(src => src.LogType))
            .ForMember(dest => dest.LogTypeValue, opt => opt.MapFrom(src => src.LogTypeValue))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

        CreateMap<LogLibDm, LogLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectId))
            .ForMember(dest => dest.ObjectName, opt => opt.MapFrom(src => src.ObjectName))
            .ForMember(dest => dest.ObjectVersion, opt => opt.MapFrom(src => src.ObjectVersion))
            .ForMember(dest => dest.ObjectFirstVersionId, opt => opt.MapFrom(src => src.ObjectFirstVersionId))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => src.ObjectType))
            .ForMember(dest => dest.LogType, opt => opt.MapFrom(src => src.LogType))
            .ForMember(dest => dest.LogTypeValue, opt => opt.MapFrom(src => src.LogTypeValue))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
    }
}