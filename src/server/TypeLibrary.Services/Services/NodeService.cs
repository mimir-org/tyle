using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class NodeService : INodeService
    {
        private readonly IMapper _mapper;
        private readonly IEfNodeRepository _nodeRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEFSimpleRepository _simpleRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public NodeService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IMapper mapper, IEfNodeRepository nodeRepository, IEFSimpleRepository simpleRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _simpleRepository = simpleRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<NodeLibCm> GetNode(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get node. The id is missing value.");

            var data = await _nodeRepository.FindNode(id).FirstOrDefaultAsync();

            if (data == null)
                throw new MimirorgNotFoundException($"There is no node with id: {id}");

            if (data.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var nodeLibCm = _mapper.Map<NodeLibCm>(data);

            if (nodeLibCm == null)
                throw new MimirorgMappingException("NodeLibDm", "NodeLibCm");

            return nodeLibCm;
        }

        public Task<IEnumerable<NodeLibCm>> GetNodes()
        {
            //var nodes = _nodeRepository.GetAllNodes().Where(x => !x.Deleted).ToList()
            //    .OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var nodes = _nodeRepository.GetAllNodes().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var nodeLibCms = _mapper.Map<IEnumerable<NodeLibCm>>(nodes);

            if (nodes.Any() && (nodeLibCms == null || !nodeLibCms.Any()))
                throw new MimirorgMappingException("List<NodeLibDm>", "ICollection<NodeLibAm>");

            return Task.FromResult(nodeLibCms ?? new List<NodeLibCm>());
        }

        public async Task<NodeLibCm> CreateNode(NodeLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _nodeRepository.FindNode(dataAm.Id).FirstOrDefaultAsync();

            if (existing != null)
            {
                var errorText = $"Node '{existing.Name}' with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db";

                throw existing.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

            var nodeLibDm = _mapper.Map<NodeLibDm>(dataAm);

            if (!double.TryParse(nodeLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"Error when parsing version value '{nodeLibDm.Version}' to double.");

            if (nodeLibDm == null)
                throw new MimirorgMappingException("NodeLibAm", "NodeLibDm");

            if (nodeLibDm.Attributes != null && nodeLibDm.Attributes.Any())
                _attributeRepository.Attach(nodeLibDm.Attributes, EntityState.Unchanged);

            if (nodeLibDm.Simples != null && nodeLibDm.Simples.Any())
                _simpleRepository.Attach(nodeLibDm.Simples, EntityState.Unchanged);

            await _nodeRepository.CreateAsync(nodeLibDm);
            await _nodeRepository.SaveAsync();

            if (nodeLibDm.Simples != null && nodeLibDm.Simples.Any())
                _simpleRepository.Detach(nodeLibDm.Simples);

            if (nodeLibDm.Attributes != null && nodeLibDm.Attributes.Any())
                _attributeRepository.Detach(nodeLibDm.Attributes);

            _nodeRepository.Detach(nodeLibDm);

            return await GetNode(nodeLibDm.Id);
        }

        public async Task<NodeLibCm> UpdateNode(NodeLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update a node without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update a node when dataAm is null.");

            var nodeToUpdate = await _nodeRepository.FindNode(dataAm.Id).FirstOrDefaultAsync();

            if (nodeToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Node with id {id} does not exist, update is not possible.");

            if (nodeToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be updated.");

            if (nodeToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The node with id {id} is deleted and can not be updated.");
            
            var latestNodeDm = GetLatestNodeVersion(nodeToUpdate.FirstVersionId);

            var latestNodeVersion = double.Parse(latestNodeDm.Version, CultureInfo.InvariantCulture);
            var nodeToUpdateVersion = double.Parse(nodeToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestNodeVersion > nodeToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update node with id {nodeToUpdate.Id} and version {nodeToUpdateVersion}. Latest version is node with id {latestNodeDm.Id} and version {latestNodeVersion}");

            dataAm.Version = IncrementNodeVersion(latestNodeDm, dataAm);
            dataAm.FirstVersionId = latestNodeDm.FirstVersionId;
            
            return await CreateNode(dataAm);
        }
        
        public async Task<bool> DeleteNode(string id)
        {
            var dm = await _nodeRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The node with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _nodeRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _nodeRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _simpleRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }

        #region Private

        private NodeLibDm GetLatestNodeVersion(string firstVersionId)
        {
            var existingDmVersions = _nodeRepository.GetAllNodes()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (!existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No nodes with 'FirstVersionId' {firstVersionId} found.");

            return existingDmVersions[^1];
        }

        // ReSharper disable once ReplaceWithSingleAssignment.False
        // ReSharper disable once ConvertIfToOrExpression
        private static string IncrementNodeVersion(NodeLibDm existing, NodeLibAm updated)
        {
            var major = false;
            var minor = false;

            if (existing.Name != updated.Name)
                throw new MimirorgBadRequestException("You cannot change existing name when updating.");

            if (existing.RdsCode != updated.RdsCode)
                throw new MimirorgBadRequestException("You cannot change existing RDS code when updating.");

            if (existing.RdsName != updated.RdsName)
                throw new MimirorgBadRequestException("You cannot change existing RDS code when updating.");

            if (existing.Aspect != updated.Aspect)
                throw new MimirorgBadRequestException("You cannot change existing Aspect when updating.");

            //PurposeName
            if (existing.PurposeName != updated.PurposeName)
                minor = true;

            //CompanyId
            if (existing.CompanyId != updated.CompanyId)
                minor = true;

            //Simple
            var simpleIdDmList = existing.Simples?.Select(x => x.Id).ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var simpleIdAmList = updated.SimpleIdList?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            if (simpleIdAmList?.Count < simpleIdDmList?.Count)
                throw new MimirorgBadRequestException("You cannot remove existing simple when updating, only add.");

            if (simpleIdAmList?.Count >= simpleIdDmList?.Count)
            {
                if (simpleIdAmList.Where((t, i) => t != simpleIdDmList[i]).Any())
                    throw new MimirorgBadRequestException("You cannot change existing simple when updating, only add.");
            }

            if (simpleIdAmList?.Count > simpleIdDmList?.Count)
                major = true;

            //Attribute
            var attributeIdDmList = existing.Attributes?.Select(x => x.Id).ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var attributeIdAmList = updated.AttributeIdList?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            if (attributeIdAmList?.Count < attributeIdDmList?.Count)
                throw new MimirorgBadRequestException("You cannot remove existing attributes when updating, only add.");

            if (attributeIdAmList?.Count >= attributeIdDmList?.Count)
            {
                if (attributeIdAmList.Where((t, i) => t != attributeIdDmList[i]).Any())
                    throw new MimirorgBadRequestException("You cannot change existing attributes when updating, only add.");
            }

            if (attributeIdAmList?.Count > attributeIdDmList?.Count)
                major = true;

            //NodeTerminals
            var nodeTerminalsAmList = updated.NodeTerminals?.ToList().OrderBy(x => x?.TerminalId, StringComparer.InvariantCulture).ToList();
            var nodeTerminalsDmList = existing.NodeTerminals?.ToList().OrderBy(x => x?.TerminalId, StringComparer.InvariantCulture).ToList();

            if (nodeTerminalsAmList?.Count < nodeTerminalsDmList?.Count)
                throw new MimirorgBadRequestException("You cannot remove existing node terminals when updating, only add.");

            if (nodeTerminalsAmList?.Count >= nodeTerminalsDmList?.Count)
            {
                for (var i = 0; i < nodeTerminalsDmList.Count; i++)
                {
                    if (nodeTerminalsAmList[i].TerminalId != nodeTerminalsDmList[i].TerminalId)
                        throw new MimirorgBadRequestException("You cannot change existing node terminal's terminal id when updating, only add.");

                    if (nodeTerminalsAmList[i].Number != nodeTerminalsDmList[i].Number)
                        throw new MimirorgBadRequestException("You cannot change existing node terminal's number when updating, only add.");

                    if (nodeTerminalsAmList[i].ConnectorDirection != nodeTerminalsDmList[i].ConnectorDirection)
                        throw new MimirorgBadRequestException("You cannot change existing node terminal's direction when updating, only add.");
                }
            }

            if (nodeTerminalsAmList?.Count > nodeTerminalsDmList?.Count)
                major = true;

            //SelectedAttributePredefined
            var selectedAttributePredefinedLibAmList = updated.SelectedAttributePredefined?.ToList().OrderBy(x => x.Key, StringComparer.InvariantCulture).ToList();
            var selectedAttributePredefinedLibDmList = existing.SelectedAttributePredefined?.ToList().OrderBy(x => x.Key, StringComparer.InvariantCulture).ToList();

            if (selectedAttributePredefinedLibAmList?.Count < selectedAttributePredefinedLibDmList?.Count)
                throw new MimirorgBadRequestException("You cannot remove existing predefined selected attributes when updating, only add.");

            if (selectedAttributePredefinedLibAmList?.Count >= selectedAttributePredefinedLibDmList?.Count)
            {
                for (var i = 0; i < selectedAttributePredefinedLibDmList.Count; i++)
                {
                    if (selectedAttributePredefinedLibAmList[i].Key != selectedAttributePredefinedLibDmList[i].Key)
                        throw new MimirorgBadRequestException("You cannot change existing predefined selected attribute key when updating, only add.");

                    if (selectedAttributePredefinedLibAmList[i].IsMultiSelect != selectedAttributePredefinedLibDmList[i].IsMultiSelect)
                        throw new MimirorgBadRequestException("You cannot change existing multi select value for predefined selected attributes when updating, only add.");

                    if (selectedAttributePredefinedLibAmList[i].Values.Count != selectedAttributePredefinedLibDmList[i].Values.Count)
                        throw new MimirorgBadRequestException("You cannot add/remove existing values (dictionary) for predefined selected attributes when updating.");

                    if (!selectedAttributePredefinedLibAmList[i].Values.ContentEquals(selectedAttributePredefinedLibDmList[i].Values))
                        throw new MimirorgBadRequestException("You cannot add/remove/change existing key values (dictionary) for predefined selected attributes when updating.");

                    var contentReferencesAm = selectedAttributePredefinedLibAmList[i]?.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
                    var contentReferencesDm = selectedAttributePredefinedLibDmList[i]?.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

                    if (contentReferencesAm == null && contentReferencesDm == null)
                        continue;

                    if (contentReferencesAm?.Count != contentReferencesDm?.Count)
                        throw new MimirorgBadRequestException("You cannot add/remove existing content references for predefined selected attributes when updating.");

                    if (contentReferencesAm.Where((t, j) => t != contentReferencesDm[j]).Any())
                        throw new MimirorgBadRequestException("You cannot change existing content references for predefined selected attributes when updating.");
                }
            }

            if (selectedAttributePredefinedLibAmList?.Count > selectedAttributePredefinedLibDmList?.Count)
                major = true;

            //Description
            if (existing.Description != updated.Description)
                minor = true;

            //Symbol
            if (existing.Symbol != updated.Symbol)
                minor = true;

            //AttributeAspectIri
            if (existing.AttributeAspectIri != updated.AttributeAspectIri)
                minor = true;

            //ContentReferences (Node)
            var nodeContentRefsAm = updated.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var nodeContentRefsDm = existing.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            if (nodeContentRefsAm?.Count != nodeContentRefsDm?.Count)
                minor = true;

            if (nodeContentRefsAm != null && nodeContentRefsDm != null && nodeContentRefsAm.Count == nodeContentRefsDm.Count)
            {
                if (nodeContentRefsDm.Where((t, i) => t != nodeContentRefsAm[i]).Any())
                    minor = true;
            }

            //ParentId
            if (existing.ParentId != updated.ParentId)
                throw new MimirorgBadRequestException("You cannot change existing node parent id when updating.");

            if (!major && !minor)
                throw new MimirorgBadRequestException("Existing node and updated node is identical");

            return major ? existing.Version.IncrementMajorVersion() : existing.Version.IncrementMinorVersion();
        }

        #endregion Private
    }
}