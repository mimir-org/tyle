using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class SimpleProfile : Profile
    {
        public SimpleProfile()
        {
            CreateMap<SimpleLibAm, SimpleLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.CreateMd5()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => CreateAttributes(src.AttributeIdList.ToList()).ToList()))
                .ForMember(dest => dest.Nodes, opt => opt.Ignore());

            CreateMap<SimpleLibDm, SimpleLibAm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri));
        }

        private static IEnumerable<AttributeLibDm> CreateAttributes(IReadOnlyCollection<string> attributes)
        {
            if (attributes == null || !attributes.Any())
                yield break;

            foreach (var item in attributes)
                yield return new AttributeLibDm
                {
                    Id = item
                };
        }
    }
}