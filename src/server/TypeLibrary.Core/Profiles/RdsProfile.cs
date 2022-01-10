using AutoMapper;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Core.Profiles
{
    public class RdsProfile : Profile
    {
        public RdsProfile()
        {
            CreateMap<CreateRds, Rds>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.RdsCategoryId, opt => opt.MapFrom(src => src.RdsCategoryId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference));
        }
    }
}
