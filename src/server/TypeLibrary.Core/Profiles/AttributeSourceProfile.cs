using System.Collections.Generic;
using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles
{
    public class AttributeSourceProfile : Profile
    {
        public AttributeSourceProfile()
        {
            CreateMap<AttributeSourceLibDm, AttributeSourceLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.TypeReferences, opt => opt.MapFrom(src => src.TypeReferences.ConvertToObject<ICollection<TypeReferenceCm>>()));
        }
    }
}