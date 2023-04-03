using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class NodeProfile : Profile
{
    public NodeProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<NodeLibAm, NodeLibDm>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.Ignore())
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.FirstVersionId, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? "Unknown" : contextAccessor.GetUserId()))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
            .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
            .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => CreateTerminals(src.NodeTerminals).ToList()))
            .ForMember(dest => dest.NodeAttributes, opt => opt.MapFrom(src => src.NodeAttributes))
            .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<NodeLibDm, NodeLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
            .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.RdsCode))
            .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
            .ForMember(dest => dest.ParentIri, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Iri : null))
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => src.NodeTerminals))
            .ForMember(dest => dest.NodeAttributes, opt => opt.MapFrom(src => src.NodeAttributes))
            .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<NodeLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Node"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }

    private static IEnumerable<NodeTerminalLibDm> CreateTerminals(ICollection<NodeTerminalLibAm> terminals)
    {
        if (terminals == null || !terminals.Any())
            yield break;

        var sortedTerminalTypes = new List<NodeTerminalLibAm>();

        foreach (var item in terminals)
        {
            var existingSortedTerminalType = sortedTerminalTypes.FirstOrDefault(x => x.TerminalId == item.TerminalId && x.ConnectorDirection == item.ConnectorDirection);

            if (existingSortedTerminalType == null)
            {
                sortedTerminalTypes.Add(item);
            }

            else
            {
                existingSortedTerminalType.MinQuantity += item.MinQuantity;
                existingSortedTerminalType.MaxQuantity += item.MaxQuantity;
            }
        }

        foreach (var item in sortedTerminalTypes)
        {
            yield return new NodeTerminalLibDm
            {
                TerminalId = item.TerminalId,
                MinQuantity = item.MinQuantity,
                MaxQuantity = item.MaxQuantity,
                ConnectorDirection = item.ConnectorDirection
            };
        }
    }
}