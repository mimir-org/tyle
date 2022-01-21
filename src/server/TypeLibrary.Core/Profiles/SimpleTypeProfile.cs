using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class SimpleTypeProfile : Profile
    {
        public SimpleTypeProfile()
        {
            CreateMap<SimpleTypeAm, SimpleType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.AttributeTypes, opt => opt.MapFrom(src => CreateAttributeTypes(src.AttributeTypes.ToList()).ToList()))
                .ForMember(dest => dest.NodeTypes, opt => opt.Ignore());

            CreateMap<SimpleType, SimpleTypeAm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri));
        }

        private static IEnumerable<AttributeType> CreateAttributeTypes(IReadOnlyCollection<string> attributeTypes)
        {
            if (attributeTypes == null || !attributeTypes.Any())
                yield break;

            foreach (var item in attributeTypes)
                yield return new AttributeType
                {
                    Id = item
                };
        }
    }
}