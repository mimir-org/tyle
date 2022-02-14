using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class TerminalProfile : Profile
    {
        public TerminalProfile()
        {
            CreateMap<TerminalLibAm, TerminalLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.ConvertToObject));
        }
    }
}