using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.Common.Extensions;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Core.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<TypeAm, NodeDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.LocationType, opt => opt.MapFrom(src => src.LocationType))
                .ForMember(dest => dest.SymbolId, opt => opt.MapFrom(src => src.SymbolId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.MapFrom(src => CreateTerminalTypes(src.TerminalTypes.ToList(), $"{src.Key}-{src.Domain}".CreateMd5()).ToList()))
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.SimpleTypes, opt => opt.MapFrom(src => SimpleTypes(src.SimpleTypes.ToList()).ToList()))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.PurposeDm, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .AfterMap((_, dest, _) =>
                {
                    dest.ResolvePredefinedAttributeData();
                });

            CreateMap<TypeAm, TransportDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.PurposeDm, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<TypeAm, InterfaceDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Key}-{src.Domain}-{src.Version}".CreateMd5()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.AttributeList, opt => opt.MapFrom(src => CreateAttributes(src.AttributeStringList.ToList()).ToList()))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.PurposeDm, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<NodeDm, TypeAm>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.ObjectBlock))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
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

            CreateMap<TransportDm, TypeAm>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.Transport))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeStringList, opt => opt.MapFrom(src => src.AttributeList.Select(x => x.Id)))
                .ForMember(dest => dest.SimpleTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LocationType, opt => opt.Ignore())
                .ForMember(dest => dest.SymbolId, opt => opt.Ignore())
                .ForMember(dest => dest.PredefinedAttributes, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<InterfaceDm, TypeAm>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => ObjectType.Interface))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.TerminalTypes, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeStringList, opt => opt.MapFrom(src => src.AttributeList.Select(x => x.Id)))
                .ForMember(dest => dest.SimpleTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LocationType, opt => opt.Ignore())
                .ForMember(dest => dest.SymbolId, opt => opt.Ignore())
                .ForMember(dest => dest.PredefinedAttributes, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<NodeTerminalDm, TerminalCm>()
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalTypeId))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.ConnectorType, opt => opt.MapFrom(src => src.ConnectorType));

            CreateMap<NodeDm, NodeCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.RdsDm.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.RdsDm.RdsCategoryDm.Name))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeList))
                .ForMember(dest => dest.SymbolId, opt => opt.MapFrom(src => src.SymbolId))
                .ForMember(dest => dest.PurposeDm, opt => opt.MapFrom(src => src.PurposeDm))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            CreateMap<TransportDm, TransportCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.RdsDm.Code))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.RdsDm.RdsCategoryDm.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.TerminalId, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.AttributeList))
                .ForMember(dest => dest.PurposeDm, opt => opt.MapFrom(src => src.PurposeDm))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            
            CreateMap<InterfaceDm, InterfaceCm>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rds, opt => opt.MapFrom(src => src.RdsDm.Code))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.RdsDm.RdsCategoryDm.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.SemanticReference, opt => opt.MapFrom(src => src.SemanticReference))
                .ForMember(dest => dest.TerminalId, opt => opt.Ignore())
                .ForMember(dest => dest.TerminalTypeId, opt => opt.MapFrom(src => src.TerminalId))
                .ForMember(dest => dest.PurposeDm, opt => opt.MapFrom(src => src.PurposeDm))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
        }

        private static IEnumerable<NodeTerminalDm> CreateTerminalTypes(IReadOnlyCollection<TerminalCm> terminalTypes, string nodeId)
        {
            if (terminalTypes == null || !terminalTypes.Any())
                yield break;

            var sortedTerminalTypes = new List<TerminalCm>();

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
                yield return new NodeTerminalDm
                {
                    Id = key.CreateMd5(),
                    NodeTypeId = nodeId,
                    TerminalTypeId = item.TerminalTypeId,
                    Number = item.Number,
                    ConnectorType = item.ConnectorType
                };
            }
        }

        private static IEnumerable<AttributeDm> CreateAttributes(IReadOnlyCollection<string> attributes)
        {
            if (attributes == null || !attributes.Any())
                yield break;

            foreach (var item in attributes)
                yield return new AttributeDm
                {
                    Id = item
                };
        }

        private static IEnumerable<SimpleDm> SimpleTypes(IReadOnlyCollection<string> simpleTypes)
        {
            if (simpleTypes == null || !simpleTypes.Any())
                yield break;
            foreach (var item in simpleTypes)
                yield return new SimpleDm
                {
                    Id = item
                };
        }
    }
}