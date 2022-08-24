using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Factories;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile(IApplicationSettingsRepository settings, IUnitFactory unitFactory, IHttpContextAccessor contextAccessor)
        {
            CreateMap<AttributeLibAm, AttributeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifier, opt => opt.MapFrom(src => src.AttributeQualifier))
                .ForMember(dest => dest.AttributeSource, opt => opt.MapFrom(src => src.AttributeSource))
                .ForMember(dest => dest.AttributeCondition, opt => opt.MapFrom(src => src.AttributeCondition))
                .ForMember(dest => dest.AttributeFormat, opt => opt.MapFrom(src => src.AttributeFormat))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/attribute/{src.Id}"))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToString()))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => ResolveUnits(src.UnitIdList, unitFactory).ToList()))
                .ForMember(dest => dest.SelectValues, opt => opt.Ignore())
                .ForMember(dest => dest.SelectValuesString, opt => opt.MapFrom(src => src.SelectValues.ConvertToString()))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetEmail()) ? "Unknown" : contextAccessor.GetEmail()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()));

            CreateMap<AttributeLibDm, AttributeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.AttributeQualifier, opt => opt.MapFrom(src => src.AttributeQualifier))
                .ForMember(dest => dest.AttributeSource, opt => opt.MapFrom(src => src.AttributeSource))
                .ForMember(dest => dest.AttributeCondition, opt => opt.MapFrom(src => src.AttributeCondition))
                .ForMember(dest => dest.AttributeFormat, opt => opt.MapFrom(src => src.AttributeFormat))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
                .ForMember(dest => dest.SelectValues, opt => opt.MapFrom(src => src.SelectValues))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
        }

        private IEnumerable<UnitLibAm> ResolveUnits(ICollection<string> unitIdList, IUnitFactory unitFactory)
        {
            if (unitIdList == null || unitFactory == null)
                yield break;

            foreach (var id in unitIdList)
            {
                var unit = unitFactory.Get(id);
                if (unit == null)
                    continue;

                yield return new UnitLibAm { Name = unit.Name };

            }
        }
    }
}