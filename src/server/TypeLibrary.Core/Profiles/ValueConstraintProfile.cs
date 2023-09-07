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

namespace TypeLibrary.Core.Profiles;

public class ValueConstraintProfile : Profile
{
    public ValueConstraintProfile()
    {
        CreateMap<ValueConstraint, ValueConstraintLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ConstraintType, opt => opt.MapFrom(src => src.ConstraintType))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.AllowedValues, opt => opt.MapFrom(src => src.AllowedValues))
            .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.DataType))
            .ForMember(dest => dest.MinCount, opt => opt.MapFrom(src => src.MinCount))
            .ForMember(dest => dest.MaxCount, opt => opt.MapFrom(src => src.MaxCount))
            .ForMember(dest => dest.Pattern, opt => opt.MapFrom(src => src.Pattern))
            .ForMember(dest => dest.MinValue, opt => opt.MapFrom(src => src.MinValue))
            .ForMember(dest => dest.MaxValue, opt => opt.MapFrom(src => src.MaxValue))
            .ForMember(dest => dest.MinInclusive, opt => opt.MapFrom(src => src.MinInclusive))
            .ForMember(dest => dest.MaxInclusive, opt => opt.MapFrom(src => src.MaxInclusive));
    }
}