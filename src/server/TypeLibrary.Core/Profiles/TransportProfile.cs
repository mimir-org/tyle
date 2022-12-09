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

namespace TypeLibrary.Core.Profiles
{
    public class TransportProfile : Profile
    {
        public TransportProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
        {
            CreateMap<TransportLibAm, TransportLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToString()))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/transport/{src.Id}"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
                .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
                .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.ParentId) ? null : src.ParentId))
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? "Unknown" : contextAccessor.GetUserId()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.ConvertToString()));

            CreateMap<TransportLibDm, TransportLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
                .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
                .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
                .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.ParentId) ? null : src.ParentId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.Terminal))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.ConvertToObject<ICollection<AttributeLibCm>>()));

            CreateMap<TransportLibCm, ApprovalCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Transport"))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
        }
    }
}