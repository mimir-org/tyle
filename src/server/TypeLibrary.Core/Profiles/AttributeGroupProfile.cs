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

namespace TypeLibrary.Core.Profiles
{
    public class AttributeGroupProfile : Profile
    {
        public AttributeGroupProfile(IHttpContextAccessor contextAccessor)
        {
            CreateMap<AttributeGroupLibAm, AttributeGroupLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Attributes, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeGroupAttributes, opt => opt.Ignore());

            CreateMap<AttributeGroupLibDm, AttributeGroupLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeGroupAttributes));

            CreateMap<AttributeGroupAttributesLibDm, AttributeLibCm>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Attribute.Id))
                .ForMember(dest => dest.State, opt => opt.Ignore())
                .ForMember(dest => dest.TypeReference, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeUnits, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Iri, opt => opt.Ignore())
                .ForMember(dest => dest.Kind, opt => opt.Ignore());


            CreateMap<AttributeGroupLibCm, ApprovalCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "AttributeGroup"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.State, opt => opt.Ignore())
                .ForMember(dest => dest.StateName, opt => opt.Ignore());
        }
    }
}