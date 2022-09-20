using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Services.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IAttributePredefinedRepository _attributePredefinedRepository;
        private readonly IQuantityDatumRepository _datumRepository;
        private readonly IAttributeReferenceRepository _attributeReferenceRepository;
        private readonly IVersionService _versionService;
        private readonly ITimedHookService _hookService;

        public AttributeService(IMapper mapper, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, IAttributePredefinedRepository attributePredefinedRepository, IAttributeReferenceRepository attributeReferenceRepository, IVersionService versionService, ITimedHookService hookService, IQuantityDatumRepository datumRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeReferenceRepository = attributeReferenceRepository;
            _versionService = versionService;
            _hookService = hookService;
            _datumRepository = datumRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        #region Attribute

        /// <summary>
        /// Get all attributes by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="includeDeleted"></param>
        /// <returns>List of AttributeLibCm</returns>
        public IEnumerable<AttributeLibCm> GetAll(Aspect aspect, bool includeDeleted = false)
        {
            var attributes = includeDeleted
                ? _attributeRepository.Get().ToList().OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList()
                : _attributeRepository.Get().Where(x => x.State != State.Deleted).ToList().OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (aspect != Aspect.NotSet)
                attributes = attributes.Where(x => x.Aspect.HasFlag(aspect)).ToList();

            return _mapper.Map<List<AttributeLibCm>>(attributes);
        }

        /// <summary>
        /// Find attribute by id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>The attribute, otherwise return null</returns>
        public async Task<AttributeLibCm> Get(string id)
        {
            var item = await _attributeRepository.Get(id);

            if (item == null)
                return null;

            var attribute = _mapper.Map<AttributeLibCm>(item);
            return attribute;
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task Create(List<AttributeLibAm> attributes, bool createdBySystem = false)
        {
            if (attributes == null || !attributes.Any())
                return;

            if (attributes.ValidateObjects().Any(x => !x.IsValid))
                throw new MimirorgBadRequestException("One or more attributes is not valid.");

            var data = _mapper.Map<List<AttributeLibDm>>(attributes);
            var existing = _attributeRepository.Get().ToList();
            var notExisting = data.Exclude(existing, x => x.Id).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributeRepository.Create(attribute, createdBySystem ? State.ApprovedGlobal : State.Draft);
            }
        }

        public async Task<AttributeLibCm> Create(AttributeLibAm attribute, bool resetVersion = false)
        {
            if (attribute == null)
                throw new MimirorgBadRequestException("Can't create an attribute that is null.");

            var status = attribute.ValidateObject();
            if (!status.IsValid)
                throw new MimirorgBadRequestException("The attribute is not valid.", status);

            if (resetVersion)
            {
                attribute.FirstVersionId = attribute.Id;
                attribute.Version = "1.0";
            }

            var data = _mapper.Map<AttributeLibDm>(attribute);

            var exist = await _attributeRepository.Exist(data.Id);

            if (exist)
                throw new MimirorgBadRequestException($"The attribute with Id: {data.Id} already exist", new Validation
                {
                    IsValid = false,
                    Message = $"The attribute with Id: {data.Id} already exist.",
                    Result = new List<ValidationResult>
                    {
                        new ValidationResult("A combination of these properties already exists.", new List<string>
                        {
                            nameof(AttributeLibAm.Name),
                            "Aspect",
                            "AttributeQualifier",
                            "AttributeSource",
                            "AttributeCondition"
                        })
                    }
                });

            await _attributeRepository.Create(data, State.Draft);
            var attrLibCm = _mapper.Map<AttributeLibCm>(data);
            return attrLibCm;
        }

        public async Task<IEnumerable<AttributeLibCm>> GetLatestVersions(Aspect aspect)
        {
            var distinctFirstVersionIdDm = aspect is Aspect.None or Aspect.NotSet ?
                _attributeRepository.Get().Where(x => x.State != State.Deleted).LatestVersion().ToList().OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList() :
                _attributeRepository.Get().Where(x => x.State != State.Deleted && x.Aspect == aspect).LatestVersion().ToList().OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            distinctFirstVersionIdDm = distinctFirstVersionIdDm.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var attributeLibCms = _mapper.Map<List<AttributeLibCm>>(distinctFirstVersionIdDm);
            return await Task.FromResult(attributeLibCms ?? new List<AttributeLibCm>());
        }

        public async Task<AttributeLibCm> Update(AttributeLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update an attribute without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update an attribute when dataAm is null.");

            var attributeToUpdate = await _attributeRepository.Get(id);

            if (attributeToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} does not exist, update is not possible.");

            if (attributeToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The attribute with id {id} is created by the system and can not be updated.");

            if (attributeToUpdate.State == State.Deleted)
                throw new MimirorgBadRequestException($"The attribute with id {id} is deleted and can not be updated.");

            var latestAttributeDm = await _versionService.GetLatestVersion(attributeToUpdate);

            if (latestAttributeDm == null)
                throw new MimirorgBadRequestException($"Latest attribute version for attribute with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestAttributeDm.Version))
                throw new MimirorgBadRequestException($"Latest version for attribute with id {id} has null or empty as version number.");

            var latestAttributeVersion = double.Parse(latestAttributeDm.Version, CultureInfo.InvariantCulture);
            var attributeToUpdateVersion = double.Parse(attributeToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestAttributeVersion > attributeToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update attribute with id {attributeToUpdate.Id} and version {attributeToUpdateVersion}. Latest version is attribute with id {latestAttributeDm.Id} and version {latestAttributeVersion}");

            // Get version
            var validation = latestAttributeDm.HasIllegalChanges(dataAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = latestAttributeDm.CalculateVersionStatus(dataAm);
            if (versionStatus == VersionStatus.NoChange)
                return await Get(latestAttributeDm.Id);

            dataAm.FirstVersionId = latestAttributeDm.FirstVersionId;
            dataAm.Version = versionStatus switch
            {
                VersionStatus.Minor => latestAttributeDm.Version.IncrementMinorVersion(),
                VersionStatus.Major => latestAttributeDm.Version.IncrementMajorVersion(),
                _ => latestAttributeDm.Version
            };

            return await Create(dataAm);
        }

        public async Task<AttributeLibCm> UpdateState(string id, State state)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update an attribute without an id.");

            var attributeToUpdate = await _attributeRepository.Get(id);

            if (attributeToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} does not exist, update is not possible.");

            if (attributeToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The attribute with id {id} is created by the system and can not be updated.");

            if (attributeToUpdate.State == State.Deleted)
                throw new MimirorgBadRequestException($"The attribute with id {id} is deleted and can not be updated.");

            var latestAttributeDm = await _versionService.GetLatestVersion(attributeToUpdate);

            if (latestAttributeDm == null)
                throw new MimirorgBadRequestException($"Latest attribute version for attribute with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestAttributeDm.Version))
                throw new MimirorgBadRequestException($"Latest version for attribute with id {id} has null or empty as version number.");

            var latestAttributeVersion = double.Parse(latestAttributeDm.Version, CultureInfo.InvariantCulture);
            var attributeToUpdateVersion = double.Parse(attributeToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestAttributeVersion > attributeToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update attribute with id {attributeToUpdate.Id} and version {attributeToUpdateVersion}. Latest version is attribute with id {latestAttributeDm.Id} and version {latestAttributeVersion}");

            await _attributeRepository.UpdateState(id, state);
            _attributeRepository.ClearAllChangeTrackers();

            var cm = await Get(id);

            if (cm != null)
                _hookService.HookQueue.Enqueue(CacheKey.Attribute);

            return cm;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var deleted = await _attributeRepository.Remove(id);

                if (deleted)
                    _hookService.HookQueue.Enqueue(CacheKey.Attribute);

                return deleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> CompanyIsChanged(string nodeId, int companyId)
        {
            var node = await Get(nodeId);

            if (node == null)
                throw new MimirorgNotFoundException($"Couldn't find node with id: {nodeId}");

            return node.CompanyId != companyId;
        }

        #endregion Attribute

        #region Predefined

        /// <summary>
        /// Get predefined attributePredefinedList
        /// </summary>
        /// <returns>List of AttributePredefinedLibCm</returns>
        public IEnumerable<AttributePredefinedLibCm> GetPredefined()
        {
            var attributes = _attributePredefinedRepository.GetPredefined().Where(x => x.State != State.Deleted).ToList()
                .OrderBy(x => x.Aspect).ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<AttributePredefinedLibCm>>(attributes);
        }

        /// <summary>
        /// Create Predefined attributePredefinedList from a list
        /// </summary>
        /// <param name="predefined"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task CreatePredefined(List<AttributePredefinedLibAm> predefined, bool createdBySystem = false)
        {
            if (predefined == null || !predefined.Any())
                return;

            var data = _mapper.Map<List<AttributePredefinedLibDm>>(predefined);
            var existing = _attributePredefinedRepository.GetPredefined().ToList();
            var notExisting = data.Exclude(existing, x => x.Key).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributePredefinedRepository.CreatePredefined(attribute, createdBySystem ? State.ApprovedGlobal : State.Draft);
            }
        }

        #endregion Predefined

        #region AttributeReferences

        public async Task<IEnumerable<TypeReferenceCm>> GetAttributeReferences()
        {
            var dataSet = await _attributeReferenceRepository.Get();
            return _mapper.Map<List<TypeReferenceCm>>(dataSet);
        }

        #endregion AttributeReferences

        #region Quantity datum

        /// <summary>
        /// Get all quantity datum range specifying
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRangeSpecifying()
        {
            var dataSet = await _datumRepository.GetQuantityDatumRangeSpecifying();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum specified scopes
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedScope()
        {
            var dataSet = await _datumRepository.GetQuantityDatumSpecifiedScope();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum with specified provenances
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedProvenance()
        {
            var dataSet = await _datumRepository.GetQuantityDatumSpecifiedProvenance();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum regularity specified
        /// </summary>
        /// <returns>A collection of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRegularitySpecified()
        {
            var dataSet = await _datumRepository.GetQuantityDatumRegularitySpecified();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        #endregion Quantity datum
    }
}