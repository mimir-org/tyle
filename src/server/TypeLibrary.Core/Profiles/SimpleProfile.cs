﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;

using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;

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
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.NodeTypes, opt => opt.Ignore());

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