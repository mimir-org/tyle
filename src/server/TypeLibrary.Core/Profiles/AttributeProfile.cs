using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Linq;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Core.Profiles;

public class AttributeProfile : Profile
{
    public AttributeProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<AttributeType, AttributeTypeView>()
            .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units.Select(x => x.Unit)));

        CreateMap<AttributeTypeView, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            //.ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Attribute"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}