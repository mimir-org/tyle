using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class TerminalProfile : Profile
{
    public TerminalProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<TerminalLibAm, TerminalLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToString()))
            .ForMember(dest => dest.Iri, opt => opt.Ignore())
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.TerminalAttributes, opt => opt.MapFrom(src => src.TerminalAttributes))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? "Unknown" : contextAccessor.GetUserId()))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<TerminalLibDm, TerminalLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
            .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.TerminalAttributes, opt => opt.MapFrom(src => src.TerminalAttributes))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

        CreateMap<TerminalLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Terminal"))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore());
    }
}