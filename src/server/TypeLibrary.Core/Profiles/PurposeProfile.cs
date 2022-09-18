using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class PurposeProfile : Profile
    {
        public PurposeProfile()
        {
            CreateMap<PurposeLibDm, PurposeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri));
        }
    }
}