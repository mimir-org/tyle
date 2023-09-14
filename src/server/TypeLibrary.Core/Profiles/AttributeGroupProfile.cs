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
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeGroupAttributes.Select(x => x.Attribute)));

        }
    }
}