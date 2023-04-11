using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class AttributeProfile : Profile
{
    public AttributeProfile(IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<AttributeLibAm, AttributeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.Ignore())
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? "Unknown" : contextAccessor.GetUserId()))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.AttributeUnits))
            .ForMember(dest => dest.AttributeAspectObjects, opt => opt.Ignore())
            .ForMember(dest => dest.AttributeTerminals, opt => opt.Ignore());

        CreateMap<AttributeLibDm, AttributeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference,
                opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName,
                opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AttributeUnits, opt => opt.MapFrom(src => src.AttributeUnits));
    }
}