using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using static Mimirorg.TypeLibrary.Extensions.LibraryExtensions;

namespace TypeLibrary.Core.Profiles
{
    public class NodeProfile : Profile
    {
        public NodeProfile(IApplicationSettingsRepository settings)
        {
            CreateMap<NodeLibAm, NodeLibDm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => $"{settings.GetCurrentOntologyIri()}aspectnode/{src.Id}"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.RdsName, opt => opt.Ignore())
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.Purpose, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Version) ? src.Version : "1.0"))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.FirstVersionId) ? src.Id : src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.BlobId))
                .ForMember(dest => dest.Blob, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeAspectId, opt => opt.MapFrom(src => src.AttributeAspectId))
                .ForMember(dest => dest.AttributeAspect, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.Collections, opt => opt.MapFrom(src => Convert<CollectionLibDm>(src.CollectionIdList).ToList()))
                .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => CreateTerminals(src.NodeTerminals, src.Id).ToList()))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => Convert<AttributeLibDm>(src.AttributeIdList).ToList()))
                .ForMember(dest => dest.Simples, opt => opt.MapFrom(src => Convert<SimpleLibDm>(src.SimpleIdList).ToList()))
                .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

            CreateMap<NodeLibDm, NodeLibAm>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.BlobId))
                .ForMember(dest => dest.AttributeAspectId, opt => opt.MapFrom(src => src.AttributeAspectId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CollectionIdList, opt => opt.MapFrom(src => Convert(src.Collections).ToList()))
                .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => src.NodeTerminals))
                .ForMember(dest => dest.AttributeIdList, opt => opt.MapFrom(src => Convert(src.Attributes).ToList()))
                .ForMember(dest => dest.SimpleIdList, opt => opt.MapFrom(src => Convert(src.Simples).ToList()))
                .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));

            CreateMap<NodeLibDm, NodeLibCm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iri, opt => opt.MapFrom(src => src.Iri))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RdsId, opt => opt.MapFrom(src => src.RdsId))
                .ForMember(dest => dest.RdsName, opt => opt.MapFrom(src => src.RdsName))
                .ForMember(dest => dest.PurposeId, opt => opt.MapFrom(src => src.PurposeId))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FirstVersionId, opt => opt.MapFrom(src => src.FirstVersionId))
                .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Aspect))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.BlobId))
                .ForMember(dest => dest.Blob, opt => opt.MapFrom(src => src.Blob))
                .ForMember(dest => dest.AttributeAspectId, opt => opt.MapFrom(src => src.AttributeAspectId))
                .ForMember(dest => dest.AttributeAspect, opt => opt.MapFrom(src => src.AttributeAspect))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.NodeTerminals, opt => opt.MapFrom(src => src.NodeTerminals))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
                .ForMember(dest => dest.Simples, opt => opt.MapFrom(src => src.Simples))
                .ForMember(dest => dest.SelectedAttributePredefined, opt => opt.MapFrom(src => src.SelectedAttributePredefined));
        }

        private static IEnumerable<NodeTerminalLibDm> CreateTerminals(ICollection<NodeTerminalLibAm> terminals, string nodeId)
        {
            if (terminals == null || !terminals.Any())
                yield break;

            var sortedTerminalTypes = new List<NodeTerminalLibAm>();

            foreach (var item in terminals)
            {
                var existingSortedTerminalType = sortedTerminalTypes.FirstOrDefault(x => x.TerminalId == item.TerminalId && x.ConnectorDirection == item.ConnectorDirection);
                if (existingSortedTerminalType == null)
                    sortedTerminalTypes.Add(item);
                else
                    existingSortedTerminalType.Number += item.Number;
            }

            foreach (var item in sortedTerminalTypes)
            {
                var key = $"{item.Id}-{nodeId}";
                yield return new NodeTerminalLibDm
                {
                    Id = key.CreateMd5(),
                    NodeId = nodeId,
                    TerminalId = item.TerminalId,
                    Number = item.Number,
                    ConnectorDirection = item.ConnectorDirection
                };
            }
        }
    }
}
