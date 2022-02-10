using System.Linq;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class AttributePredefinedProfile : Profile
    {
        public AttributePredefinedProfile()
        {
            CreateMap<AttributePredefinedLibDm, AttributePredefinedLibCm>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.ValueStringList.ToDictionary(x => x, x => false)))
                .ForMember(dest => dest.IsMultiSelect, opt => opt.MapFrom(src => src.IsMultiSelect));
        }
    }
}