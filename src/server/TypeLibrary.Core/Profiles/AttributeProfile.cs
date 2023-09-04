using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Linq;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeProfile : Profile
{
    public AttributeProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<AttributeLibAm, AttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Version, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdateOn, opt => opt.Ignore())
            //.ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Predicate, opt => opt.MapFrom(src => src.Predicate))
            .ForMember(dest => dest.UoMs, opt => opt.MapFrom(src => src.UoMs))
            .ForMember(dest => dest.ProvenanceQualifier, opt => opt.MapFrom(src => src.ProvenanceQualifier))
            .ForMember(dest => dest.RangeQualifier, opt => opt.MapFrom(src => src.RangeQualifier))
            .ForMember(dest => dest.RegularityQualifier, opt => opt.MapFrom(src => src.RegularityQualifier))
            .ForMember(dest => dest.ScopeQualifier, opt => opt.MapFrom(src => src.ScopeQualifier))
            .ForMember(dest => dest.ValueConstraint, opt => opt.MapFrom(src => src.ValueConstraint))
            .ForMember(dest => dest.AttributeBlocks, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeTerminals, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeGroups, opt => opt.Ignore());

        CreateMap<AttributeLibDm, AttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.ContributedBy, opt => opt.MapFrom(src => src.ContributedBy))
            .ForMember(dest => dest.LastUpdateOn, opt => opt.MapFrom(src => src.LastUpdateOn))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Predicate, opt => opt.MapFrom(src => src.Predicate))
            .ForMember(dest => dest.UoMs, opt => opt.MapFrom(src => src.UoMs))
            .ForMember(dest => dest.ProvenanceQualifier, opt => opt.MapFrom(src => src.ProvenanceQualifier))
            .ForMember(dest => dest.RangeQualifier, opt => opt.MapFrom(src => src.RangeQualifier))
            .ForMember(dest => dest.RegularityQualifier, opt => opt.MapFrom(src => src.RegularityQualifier))
            .ForMember(dest => dest.ScopeQualifier, opt => opt.MapFrom(src => src.ScopeQualifier))
            .ForMember(dest => dest.ValueConstraint, opt => opt.MapFrom(src => src.ValueConstraint))
            .ForMember(dest => dest.AttributeGroups, opt => opt.MapFrom(src => src.AttributeGroups.Select(x => x.AttributeGroup)));

        CreateMap<AttributeLibCm, ApprovalCm>()
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