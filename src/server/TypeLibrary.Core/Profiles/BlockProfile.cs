using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.TypeLibrary.Models.Domain;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Core.Profiles;

public class BlockProfile : Profile
{
    public BlockProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
    {
        CreateMap<BlockType, BlockTypeView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)))
            .ForMember(dest => dest.Terminals, opt => opt.MapFrom(src => src.BlockTerminals))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.BlockAttributes));
            //.ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

        CreateMap<BlockTypeView, ApprovalCm>()
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

    /*private static IEnumerable<BlockTerminalRequest> CreateTerminals(ICollection<BlockTerminalRequest> terminals)
    {
        if (terminals == null || !terminals.Any())
            yield break;

        var sortedTerminalTypes = new List<BlockTerminalRequest>();

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