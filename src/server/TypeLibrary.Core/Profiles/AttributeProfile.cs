using System.Linq;
using AutoMapper;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Core.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<AttributeTypeAm, AttributeType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.Entity))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.QualifierId, opt => opt.MapFrom(src => src.QualifierId))
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.ConditionId, opt => opt.MapFrom(src => src.ConditionId))
                .ForMember(dest => dest.FormatId, opt => opt.MapFrom(src => src.FormatId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.SelectType, opt => opt.MapFrom(src => src.SelectType))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ConvertToObject))
                .ForMember(dest => dest.SelectValuesString, opt => opt.MapFrom(src => src.SelectValues == null ? null : src.SelectValues.ConvertToString()));

            CreateMap<UnitAm, Unit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference));

            CreateMap<Unit, UnitAm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference));

            CreateMap<PredefinedAttribute, PredefinedAttributeAm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values.ToDictionary(x => x, x => false)))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect));
        }
    }
}