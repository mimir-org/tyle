using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class TerminalProfile : Profile
    {
        public TerminalProfile()
        {
            CreateMap<TerminalLibAm, TerminalLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ParentTerminalId, opt => opt.MapFrom(src => src.ParentTerminalId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.ConvertToObject));
        }
    }
}