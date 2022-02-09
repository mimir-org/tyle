using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class BlobProfile : Profile
    {
        public BlobProfile()
        {
            CreateMap<BlobLibDm, BlobLibAm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline));

            CreateMap<BlobLibAm, BlobLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline));
        }
    }
}
