﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Core.Profiles
{
    public class LibraryTypeProfile : Profile
    {
        public LibraryTypeProfile()
        {
            CreateMap<CreateLibraryType, NodeType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.LocationType, opt => opt.MapFrom(src => src.LocationType))
                .ForMember(dest => dest.SymbolId, opt => opt.MapFrom(src => src.SymbolId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.MapFrom(src => CreateTerminalTypes(src.TerminalTypes.ToList(), $"{src.Key}-{src.Domain}".CreateMd5()).ToList()))
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.SimpleTypes, opt => opt.MapFrom(src => SimpleTypes(src.SimpleTypes.ToList()).ToList()))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.Purpose, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .AfterMap((_, dest, _) =>
                {
                    dest.ResolvePredefinedAttributeData();
                });

            CreateMap<CreateLibraryType, TransportType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.Purpose, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<CreateLibraryType, InterfaceType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.Purpose, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<NodeType, CreateLibraryType>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.ObjectBlock))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.MapFrom(src => src.TerminalTypes))
                .ForMember(dest => dest.AttributeStringList, opt => opt.MapFrom(src => src.AttributeList.Select(x => x.Id)))
                .ForMember(dest => dest.SimpleTypes, opt => opt.MapFrom(src => src.SimpleTypes.Select(x => x.Id)))
                .ForMember(dest => dest.LocationType, opt => opt.MapFrom(src => src.LocationType))
                .ForMember(dest => dest.SymbolId, opt => opt.MapFrom(src => src.SymbolId))
                .ForMember(dest => dest.PredefinedAttributes, opt => opt.MapFrom(src => src.PredefinedAttributes))
                .ForMember(dest => dest.TerminalTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.PurposeId))
                .BeforeMap((src, _, _) =>
                {
                    src.ResolvePredefinedAttributes();
                });

            CreateMap<TransportType, CreateLibraryType>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.Transport))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeStringList, opt => opt.MapFrom(src => src.AttributeList.Select(x => x.Id)))
                .ForMember(dest => dest.SimpleTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LocationType, opt => opt.Ignore())
                .ForMember(dest => dest.SymbolId, opt => opt.Ignore())
                .ForMember(dest => dest.PredefinedAttributes, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<InterfaceType, CreateLibraryType>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.Interface))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeStringList, opt => opt.MapFrom(src => src.AttributeList.Select(x => x.Id)))
                .ForMember(dest => dest.SimpleTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LocationType, opt => opt.Ignore())
                .ForMember(dest => dest.SymbolId, opt => opt.Ignore())
                .ForMember(dest => dest.PredefinedAttributes, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<NodeTypeTerminalType, TerminalTypeItem>()
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.ConnectorType, opt => opt.MapFrom(src => src.ConnectorType));

            CreateMap<NodeType, LibraryNodeItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.Rds.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Rds.RdsCategory.Name))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeList))
                .ForMember(dest => dest.SymbolId, opt => opt.MapFrom(src => src.SymbolId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<TransportType, LibraryTransportItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.Rds.Code))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Rds.RdsCategory.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.TerminalId, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeList))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            
            CreateMap<InterfaceType, LibraryInterfaceItem>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.Rds.Code))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Rds.RdsCategory.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.TerminalId, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
        }

        private static IEnumerable<NodeTypeTerminalType> CreateTerminalTypes(IReadOnlyCollection<TerminalTypeItem> terminalTypes, string nodeId)
        {
            if (terminalTypes == null || !terminalTypes.Any())
                yield break;

            var sortedTerminalTypes = new List<TerminalTypeItem>();

            foreach (var item in terminalTypes)
            {
                var existingSortedTerminalType = sortedTerminalTypes.FirstOrDefault(x => x.TerminalTypeId == item.TerminalTypeId && x.ConnectorType == item.ConnectorType);
                if (existingSortedTerminalType == null)
                {
                    sortedTerminalTypes.Add(item);
                }
                else
                {
                    existingSortedTerminalType.Number += item.Number;
                }
            }

            foreach (var item in sortedTerminalTypes)
            {
                var key = $"{item.Key}-{nodeId}"; 
                yield return new NodeTypeTerminalType
                {
                    Id = key.CreateMd5(),
                    NodeTypeId = nodeId,
                    TerminalTypeId = item.TerminalTypeId,
                    Number = item.Number,
                    ConnectorType = item.ConnectorType
                };
            }
        }

        private static IEnumerable<Attribute> CreateAttributes(IReadOnlyCollection<string> attributes)
        {
            if (attributes == null || !attributes.Any())
                yield break;

            foreach (var item in attributes)
                yield return new Attribute
                {
                    Id = item
                };
        }

        private static IEnumerable<SimpleType> SimpleTypes(IReadOnlyCollection<string> simpleTypes)
        {
            if (simpleTypes == null || !simpleTypes.Any())
                yield break;
            foreach (var item in simpleTypes)
                yield return new SimpleType
                {
                    Id = item
                };
        }
    }
}