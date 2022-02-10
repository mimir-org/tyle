using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<AttributeLibAm, AttributeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.Entity))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifierId, opt => opt.MapFrom(src => src.AttributeQualifierId))
                .ForMember(dest => dest.AttributeSourceId, opt => opt.MapFrom(src => src.AttributeSourceId))
                .ForMember(dest => dest.AttributeConditionId, opt => opt.MapFrom(src => src.AttributeConditionId))
                .ForMember(dest => dest.AttributeFormatId, opt => opt.MapFrom(src => src.AttributeFormatId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ConvertToObject))
                .ForMember(dest => dest.SelectValues, opt => opt.Ignore())
                .ForMember(dest => dest.SelectValuesString, opt => opt.MapFrom(src => src.SelectValues == null ? null : src.SelectValues.ConvertToString()));
        }
    }
}