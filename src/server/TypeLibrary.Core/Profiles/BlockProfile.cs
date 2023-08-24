using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Core.Factories;
using TypeLibrary.Core.Profiles.Resolvers;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class BlockProfile : Profile
{
    public BlockProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<BlockLibAm, BlockLibDm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Iri, opt => opt.MapFrom(new BlockIriResolver(settings)))
            .ForMember(dest => dest.TypeReference, opt => opt.MapFrom(src => src.TypeReference))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.FirstVersionId, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetUserId()) ? CreatedBy.Unknown : contextAccessor.GetUserId()))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.PurposeName))
            .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
            .ForMember(dest => dest.Rds, opt => opt.Ignore())
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BlockTerminals, opt => opt.MapFrom(src => CreateTerminals(src.BlockTerminals)))
            .ForMember(dest => dest.BlockAttributes, opt => opt.Ignore())
            .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<BlockLibDm, BlockLibCm>()
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
            .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
            .ForMember(dest => dest.RdsCode, opt => opt.MapFrom(src => src.Rds.RdsCode))
            .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.Rds.Name))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BlockTerminals, opt => opt.MapFrom(src => src.BlockTerminals))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.BlockAttributes.Select(x => x.Attribute)))
            .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<BlockLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Block"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }

    private static IEnumerable<BlockTerminalLibAm> CreateTerminals(ICollection<BlockTerminalLibAm> terminals)
    {
        if (terminals == null || !terminals.Any())
            yield break;

        var sortedTerminalTypes = new List<BlockTerminalLibAm>();

        foreach (var item in terminals)
        {
            var existingSortedTerminalType = sortedTerminalTypes.FirstOrDefault(x => x.TerminalId == item.TerminalId && x.Direction == item.Direction);

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
            yield return item;
        }
    }
}