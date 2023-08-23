using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using TypeLibrary.Core.Profiles.Resolvers;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeProfile : Profile
{
    public AttributeProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<AttributeLibAm, AttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(new AttributeIriResolver(settings)))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.AttributeUnits))
            .ForMember(dest => dest.AttributeBlocks, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeTerminals, opt => opt.Ignore());

        CreateMap<AttributeLibDm, AttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference,
                opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.AttributeUnits));

        CreateMap<AttributeLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Attribute"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}