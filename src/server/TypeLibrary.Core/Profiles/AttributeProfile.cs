using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile(IApplicationSettingsRepository settings)
        {
            CreateMap<AttributeLibAm, AttributeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifier, opt => opt.MapFrom(src => src.AttributeQualifier))
                .ForMember(dest => dest.AttributeSource, opt => opt.MapFrom(src => src.AttributeSource))
                .ForMember(dest => dest.AttributeCondition, opt => opt.MapFrom(src => src.AttributeCondition))
                .ForMember(dest => dest.AttributeFormat, opt => opt.MapFrom(src => src.AttributeFormat))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.GetCurrentOntologyIri()}attribute/{src.Id}"))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ConvertToObject))
                .ForMember(dest => dest.SelectValues, opt => opt.Ignore())
                .ForMember(dest => dest.SelectValuesString, opt => opt.MapFrom(src => src.SelectValues.ConvertToString()));

            CreateMap<AttributeLibDm, AttributeLibAm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifier, opt => opt.MapFrom(src => src.AttributeQualifier))
                .ForMember(dest => dest.AttributeSource, opt => opt.MapFrom(src => src.AttributeSource))
                .ForMember(dest => dest.AttributeCondition, opt => opt.MapFrom(src => src.AttributeCondition))
                .ForMember(dest => dest.AttributeFormat, opt => opt.MapFrom(src => src.AttributeFormat))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.UnitNames, opt => opt.MapFrom(src => src.Units.Select(x => x.Name).ToList()))
                .ForMember(dest => dest.SelectValues, opt => opt.MapFrom(src => src.SelectValues.ConvertToString()));

            CreateMap<AttributeLibDm, AttributeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifier, opt => opt.MapFrom(src => src.AttributeQualifier))
                .ForMember(dest => dest.AttributeSource, opt => opt.MapFrom(src => src.AttributeSource))
                .ForMember(dest => dest.AttributeCondition, opt => opt.MapFrom(src => src.AttributeCondition))
                .ForMember(dest => dest.AttributeFormat, opt => opt.MapFrom(src => src.AttributeFormat))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
                .ForMember(dest => dest.SelectValues, opt => opt.MapFrom(src => src.SelectValues.ConvertToString()));
        }
    }
}