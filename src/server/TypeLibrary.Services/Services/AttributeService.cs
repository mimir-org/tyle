using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

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
        private readonly ITimedHookService _hookService;
        private readonly ILogService _logService;

        public AttributeService(IMapper mapper, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, IAttributePredefinedRepository attributePredefinedRepository, IAttributeReferenceRepository attributeReferenceRepository, ITimedHookService hookService, IQuantityDatumRepository datumRepository, ILogService logService)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeReferenceRepository = attributeReferenceRepository;
            _hookService = hookService;
            _datumRepository = datumRepository;
            _logService = logService;
            _applicationSettings = applicationSettings?.Value;
        }

        #region Attribute

        /// <summary>
        /// Find attribute by id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>The attribute, otherwise return null</returns>
        public AttributeLibCm GetLatestVersion(string id)
        {
            var dm = _attributeRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            return _mapper.Map<AttributeLibCm>(dm);
        }

        /// <summary>
        /// Get all attributes by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns>List of AttributeLibCm</returns>
        public IEnumerable<AttributeLibCm> GetLatestVersions(Aspect aspect)
        {
            var dms = _attributeRepository.Get()?.LatestVersion()?.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (dms == null)
                throw new MimirorgNotFoundException("No attributes were found.");

            if (aspect != Aspect.NotSet)
                dms = dms.Where(x => x.Aspect.HasFlag(aspect)).ToList();

            return !dms.Any() ? new List<AttributeLibCm>() : _mapper.Map<List<AttributeLibCm>>(dms);
        }

        /// <summary>
        /// Create an new attribute
        /// </summary>
        /// <param name="attributeAm">The attribute that should be created</param>
        /// <returns>The created attribute</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if attribute is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if attribute already exist</exception>
        /// <remarks>Remember that creating a new attribute could be creating a new version of existing attribute.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<AttributeLibCm> Create(AttributeLibAm attributeAm)
        {
            if (attributeAm == null)
                throw new ArgumentNullException(nameof(attributeAm));

            var validation = attributeAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", validation);

            if (await _attributeRepository.Exist(attributeAm.Id))
                throw new MimirorgDuplicateException($"Attribute '{attributeAm.Name}' and version '{attributeAm.Version}' already exist.");

            attributeAm.Version = "1.0";
            var dm = _mapper.Map<AttributeLibDm>(attributeAm);

            dm.State = State.Draft;
            dm.FirstVersionId = dm.Id;

            await _attributeRepository.Create(dm);
            _attributeRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
            _hookService.HookQueue.Enqueue(CacheKey.Attribute);

            return GetLatestVersion(dm.Id);
        }

        /// <summary>
        /// Update an attribute if the data is allowed to be changed.
        /// </summary>
        /// <param name="attributeAm">The attribute to update</param>
        /// <returns>The updated attribute</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the attribute does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<AttributeLibCm> Update(AttributeLibAm attributeAm)
        {
            var validation = attributeAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", validation);

            var attributeToUpdate = _attributeRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == attributeAm.Id);

            if (attributeToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(AttributeLibAm.Name), nameof(AttributeLibAm.Version) },
                $"Attribute with name {attributeAm.Name}, aspect {attributeAm.Aspect}, QuantityDatumSpecifiedScope {attributeAm.QuantityDatumSpecifiedScope}, QuantityDatumSpecifiedProvenance {attributeAm.QuantityDatumSpecifiedProvenance}, QuantityDatumRangeSpecifying {attributeAm.QuantityDatumRangeSpecifying}, QuantityDatumRegularitySpecified {attributeAm.QuantityDatumRegularitySpecified}, id {attributeAm.Id} and version {attributeAm.Version} does not exist.");
                throw new MimirorgBadRequestException("Attribute does not exist. Update is not possible.", validation);
            }

            validation = attributeToUpdate.HasIllegalChanges(attributeAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = attributeToUpdate.CalculateVersionStatus(attributeAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(attributeToUpdate.Id);

            attributeAm.Version = versionStatus switch
            {
                VersionStatus.Minor => attributeToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => attributeToUpdate.Version.IncrementMajorVersion(),
                _ => attributeToUpdate.Version
            };

            var dm = _mapper.Map<AttributeLibDm>(attributeAm);

            dm.State = State.Draft;
            dm.FirstVersionId = attributeToUpdate.FirstVersionId;

            var attributeCm = await _attributeRepository.Create(dm);
            _attributeRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
            _hookService.HookQueue.Enqueue(CacheKey.Attribute);

            return GetLatestVersion(attributeCm.Id);
        }

        /// <summary>
        /// Change attribute state
        /// </summary>
        /// <param name="id">The attribute id that should change the state</param>
        /// <param name="state">The new attribute state</param>
        /// <returns>Attribute with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the attribute does not exist on latest version</exception>
        public async Task<AttributeLibCm> ChangeState(string id, State state)
        {
            var dm = _attributeRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found, or is not latest version.");

            var newStateDms = _attributeRepository.Get().Where(x => x.FirstVersionId == dm.FirstVersionId && x.State != state).ToList();

            if (!newStateDms.Any())
                return null;

            await _attributeRepository.ChangeState(state, newStateDms.Select(x => x.Id).ToList());

            foreach (var newStateDm in newStateDms)
                await _logService.CreateLog(newStateDm, LogType.State, state.ToString());

            _hookService.HookQueue.Enqueue(CacheKey.Attribute);

            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get attribute existing company id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>Company id for attribute</returns>
        public async Task<int> GetCompanyId(string id)
        {
            return await _attributeRepository.HasCompany(id);
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
        /// <returns></returns>
        public async Task CreatePredefined(List<AttributePredefinedLibAm> predefined)
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
                attribute.CreatedBy = _applicationSettings.System;
                attribute.State = State.ApproveGlobal;
                await _attributePredefinedRepository.CreatePredefined(attribute);
            }
        }

        #endregion Predefined

        #region AttributeReferences

        /// <summary>
        /// Get attribute references
        /// </summary>
        /// <returns>A List of attribute references</returns>
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