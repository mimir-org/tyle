using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using static Mimirorg.TypeLibrary.Extensions.LibraryExtensions;

namespace TypeLibrary.Core.Profiles
{
    public class NodeProfile : Profile
    {
        public NodeProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
        {
            CreateMap<NodeLibAm, NodeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/aspectnode/{src.Id}"))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToUriString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
                .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
                .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Version) ? src.Version : "1.0"))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => State.Draft))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.AttributeAspectIri, opt => opt.MapFrom(src => src.AttributeAspectIri))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetName()) ? "Unknown" : contextAccessor.GetName()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => CreateTerminals(src.NodeTerminals, src.Id).ToList()))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => Convert<AttributeLibDm>(src.AttributeIdList).ToList()))
                .ForMember(dest => dest.Simples, opt => opt.MapFrom(src => Convert<SimpleLibDm>(src.SimpleIdList).ToList()))
                .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

            CreateMap<NodeLibDm, NodeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.ContentReferences, opt => opt.MapFrom(src => src.ContentReferences.ConvertToArray()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
                .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
                .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
                .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.AttributeAspectIri, opt => opt.MapFrom(src => src.AttributeAspectIri))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => src.NodeTerminals))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
                .ForMember(dest => dest.Simples, opt => opt.MapFrom(src => src.Simples))
                .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));
        }

        private static IEnumerable<NodeTerminalLibDm> CreateTerminals(ICollection<NodeTerminalLibAm> terminals, string nodeId)
        {
            if (terminals == null || !terminals.Any())
                yield break;

            var sortedTerminalTypes = new List<NodeTerminalLibAm>();

            foreach (var item in terminals)
            {
                var existingSortedTerminalType = sortedTerminalTypes.FirstOrDefault(x => x.TerminalId == item.TerminalId && x.ConnectorDirection == item.ConnectorDirection);
                if (existingSortedTerminalType == null)
                    sortedTerminalTypes.Add(item);
                else
                    existingSortedTerminalType.Number += item.Number;
            }

            foreach (var item in sortedTerminalTypes)
            {
                var key = $"{item.Id}-{nodeId}";
                yield return new NodeTerminalLibDm
                {
                    Id = key.CreateMd5(),
                    NodeId = nodeId,
                    TerminalId = item.TerminalId,
                    Number = item.Number,
                    ConnectorDirection = item.ConnectorDirection
                };
            }
        }
    }
}