using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class BlobProfile : Profile
    {
        public BlobProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
        {
            CreateMap<BlobLibAm, BlobLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.GetCurrentOntologyIri()}blob/name/{src.Name}/discipline/{src.Discipline}"))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline));

            CreateMap<BlobLibDm, BlobLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Data) ? null : $"{contextAccessor.GetApplicationUrl()}/symbol/{src.Id}.svg"));
        }
    }
}