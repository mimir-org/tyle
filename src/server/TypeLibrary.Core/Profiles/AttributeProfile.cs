using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Core.Factories;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Factories;
using TypeLibrary.Data.Models;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Core.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile(IApplicationSettingsRepository settings, IUnitFactory unitFactory, IHttpContextAccessor contextAccessor, ICompanyFactory companyFactory)
        {
            CreateMap<AttributeLibAm, AttributeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.QuantityDatumSpecifiedScope, opt => opt.MapFrom(src => src.QuantityDatumSpecifiedScope))
                .ForMember(dest => dest.QuantityDatumSpecifiedProvenance, opt => opt.MapFrom(src => src.QuantityDatumSpecifiedProvenance))
                .ForMember(dest => dest.QuantityDatumRangeSpecifying, opt => opt.MapFrom(src => src.QuantityDatumRangeSpecifying))
                .ForMember(dest => dest.QuantityDatumRegularitySpecified, opt => opt.MapFrom(src => src.QuantityDatumRegularitySpecified))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.ApplicationSemanticUrl}/attribute/{src.Id}"))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => ResolveUnits(src.UnitIdList, unitFactory).ConvertToString()))
                .ForMember(dest => dest.SelectValues, opt => opt.Ignore())
                .ForMember(dest => dest.SelectValuesString, opt => opt.MapFrom(src => src.SelectValues.ConvertToString()))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(contextAccessor.GetEmail()) ? "Unknown" : contextAccessor.GetEmail()))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<AttributeLibDm, AttributeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.QuantityDatumSpecifiedScope, opt => opt.MapFrom(src => src.QuantityDatumSpecifiedScope))
                .ForMember(dest => dest.QuantityDatumSpecifiedProvenance, opt => opt.MapFrom(src => src.QuantityDatumSpecifiedProvenance))
                .ForMember(dest => dest.QuantityDatumRangeSpecifying, opt => opt.MapFrom(src => src.QuantityDatumRangeSpecifying))
                .ForMember(dest => dest.QuantityDatumRegularitySpecified, opt => opt.MapFrom(src => src.QuantityDatumRegularitySpecified))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => companyFactory.GetCompanyName(src.CompanyId)))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Select, opt => opt.MapFrom(src => src.Select))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units.ConvertToObject<ICollection<UnitLibCm>>()))
                .ForMember(dest => dest.SelectValues, opt => opt.MapFrom(src => src.SelectValues))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
        }

        private IEnumerable<UnitLibDm> ResolveUnits(ICollection<string> unitIdList, IUnitFactory unitFactory)
        {
            if (unitIdList == null || unitFactory == null)
                yield break;

            foreach (var id in unitIdList)
            {
                var unit = unitFactory.Get(id);
                if (unit == null)
                    continue;

                yield return unit;

            }
        }
    }
}