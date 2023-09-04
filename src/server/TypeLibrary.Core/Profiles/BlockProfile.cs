using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles;

public class BlockProfile : Profile
{
    public BlockProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<BlockLibAm, BlockType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Version, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.CreatedBy,
                opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(contextAccessor.GetUserId())
                        ? CreatedBy.Unknown
                        : contextAccessor.GetUserId()))
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdateOn, opt => opt.Ignore())
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            //.ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
            .ForMember(dest => dest.Notation, opt => opt.MapFrom(src => src.Notation))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.BlockTerminals, opt => opt.MapFrom(src => src.BlockTerminals))
            .ForMember(dest => dest.BlockAttributes, opt => opt.MapFrom(src => src.BlockAttributes));
            //.ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<BlockType, BlockLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.ContributedBy, opt => opt.MapFrom(src => src.ContributedBy))
            .ForMember(dest => dest.LastUpdateOn, opt => opt.MapFrom(src => src.LastUpdateOn))
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
            .ForMember(dest => dest.Notation, opt => opt.MapFrom(src => src.Notation))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
            .ForMember(dest => dest.BlockTerminals, opt => opt.MapFrom(src => src.BlockTerminals))
            .ForMember(dest => dest.BlockAttributes, opt => opt.MapFrom(src => src.BlockAttributes));
            //.ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<BlockLibCm, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            //.ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Block"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }

    /*private static IEnumerable<BlockTerminalLibAm> CreateTerminals(ICollection<BlockTerminalLibAm> terminals)
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
                existingSortedTerminalType.MinCount += item.MinCount;
                existingSortedTerminalType.MaxCount += item.MaxCount;
            }
        }

        foreach (var item in sortedTerminalTypes)
        {
            yield return item;
        }
    }*/
}