using System.Collections.Generic;
using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class TerminalProfile : Profile
    {
        public TerminalProfile(IApplicationSettingsRepository settings, IAttributeFactory attributeFactory)
        {
            CreateMap<TerminalLibAm, TerminalLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.GetCurrentOntologyIri()}terminal/{src.Id}"))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToUriString()))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => ResolveAttributes(src.AttributeIdList, attributeFactory)));

            CreateMap<TerminalLibDm, TerminalLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToArray()))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes));
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