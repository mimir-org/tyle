using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Factories;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class TerminalProfile : Profile
    {
        public TerminalProfile(IApplicationSettingsRepository settings, IAttributeFactory attributeFactory, IHttpContextAccessor contextAccessor)
        {
            CreateMap<TerminalLibAm, TerminalLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToString()))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/terminal/{src.Id}"))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Version) ? src.Version : "1.0"))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.FirstVersionId) ? src.FirstVersionId : src.Id))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => ResolveAttributes(src.AttributeIdList, attributeFactory)))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetEmail()) ? "Unknown" : contextAccessor.GetEmail()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()));

            CreateMap<TerminalLibDm, TerminalLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
        }

        private IEnumerable<AttributeLibAm> ResolveAttributes(ICollection<string> attributeIdList, IAttributeFactory attributeFactory)
        {
            if (attributeIdList == null || attributeFactory == null)
                yield break;

            foreach (var id in attributeIdList)
            {
                var attribute = attributeFactory.Get(id);
                if (attribute == null)
                    continue;

                yield return new AttributeLibAm
                {
                    Name = attribute.Name,
                    AttributeCondition = attribute.AttributeCondition,
                    AttributeQualifier = attribute.AttributeQualifier,
                    AttributeSource = attribute.AttributeSource,
                    Aspect = attribute.Aspect
                };
            }
        }
    }
}