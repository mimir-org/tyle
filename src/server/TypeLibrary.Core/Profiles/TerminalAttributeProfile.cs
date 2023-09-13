using System;
using System.Linq;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Core.Profiles;

public class TerminalAttributeProfile : Profile
{
    public TerminalAttributeProfile()
    {
        CreateMap<TerminalAttributeTypeReference, AttributeTypeReferenceView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttributeId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Attribute.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Attribute.CreatedOn))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Attribute.CreatedBy))
            .ForMember(dest => dest.ContributedBy, opt => opt.MapFrom(src => src.Attribute.ContributedBy))
            .ForMember(dest => dest.LastUpdateOn, opt => opt.MapFrom(src => src.Attribute.LastUpdateOn))
            .ForMember(dest => dest.Predicate, opt => opt.MapFrom(src => src.Attribute.Predicate))
            .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Attribute.Units.Select(x => x.Unit)))
            .ForMember(dest => dest.UnitMinCount, opt => opt.MapFrom(src => src.Attribute.UnitMinCount))
            .ForMember(dest => dest.UnitMaxCount, opt => opt.MapFrom(src => src.Attribute.UnitMaxCount))
            .ForMember(dest => dest.ProvenanceQualifier, opt => opt.MapFrom(src => src.Attribute.ProvenanceQualifier))
            .ForMember(dest => dest.RangeQualifier, opt => opt.MapFrom(src => src.Attribute.RangeQualifier))
            .ForMember(dest => dest.RegularityQualifier, opt => opt.MapFrom(src => src.Attribute.RegularityQualifier))
            .ForMember(dest => dest.ScopeQualifier, opt => opt.MapFrom(src => src.Attribute.ScopeQualifier))
            .ForMember(dest => dest.ValueConstraint, opt => opt.MapFrom(src => src.Attribute.ValueConstraint));
    }
}