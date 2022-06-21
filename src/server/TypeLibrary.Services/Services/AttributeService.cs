using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
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
        private readonly IAttributeQualifierRepository _attributeQualifierRepository;
        private readonly IAttributeSourceRepository _attributeSourceRepository;
        private readonly IAttributeFormatRepository _attributeFormatRepository;
        private readonly IAttributeConditionRepository _attributeConditionRepository;

        public AttributeService(IMapper mapper, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, IAttributeQualifierRepository attributeQualifierRepository, IAttributeSourceRepository attributeSourceRepository, IAttributeFormatRepository attributeFormatRepository, IAttributeConditionRepository attributeConditionRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
            _attributeQualifierRepository = attributeQualifierRepository;
            _attributeSourceRepository = attributeSourceRepository;
            _attributeFormatRepository = attributeFormatRepository;
            _attributeConditionRepository = attributeConditionRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        #region Attribute

        /// <summary>
        /// Get all attributes by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns>List of AttributeLibCm</returns>
        public IEnumerable<AttributeLibCm> Get(Aspect aspect)
        {
            var attributes = _attributeRepository.Get().ToList()
                .OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (aspect != Aspect.NotSet)
                attributes = attributes.Where(x => x.Aspect.HasFlag(aspect)).ToList();

            return _mapper.Map<List<AttributeLibCm>>(attributes);
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

            var data = _mapper.Map<List<AttributeLibDm>>(attributes);
            var existing = _attributeRepository.Get().ToList();
            var notExisting = data.Exclude(existing, x => x.Id).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributeRepository.Create(attribute);
            }
        }

        #endregion Attribute

        #region Predefined

        /// <summary>
        /// Get predefined attributePredefinedList
        /// </summary>
        /// <returns>List of AttributePredefinedLibCm</returns>
        public IEnumerable<AttributePredefinedLibCm> GetPredefined()
        {
            var attributes = _attributeRepository.GetPredefined().ToList()
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
            var existing = _attributeRepository.GetPredefined().ToList();
            var notExisting = data.Exclude(existing, x => x.Key).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributeRepository.CreatePredefined(attribute);
            }
        }

        #endregion Predefined

        #region Aspect

        public Task<IEnumerable<AttributeAspectLibCm>> GetAspects()
        {
            var allAspects = _attributeRepository.GetAspects().ToList();
            var aspects = allAspects.Where(x => x.ParentId != null).ToList();
            var topParents = allAspects.Where(x => x.ParentId == null).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var sortedAspects = aspects.OrderBy(x => topParents.FirstOrDefault(y => y.Id == x.ParentId)?.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            sortedAspects.AddRange(topParents);
            var dataCm = _mapper.Map<List<AttributeAspectLibCm>>(sortedAspects);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        /// <summary>
        /// Create new attribute aspects
        /// </summary>
        /// <param name="aspects"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task CreateAspects(List<AttributeAspectLibAm> aspects, bool createdBySystem = false)
        {
            if (aspects == null || !aspects.Any())
                return;

            var data = _mapper.Map<List<AttributeAspectLibDm>>(aspects);
            var existing = _attributeRepository.GetAspects().ToList();
            var notExisting = data.Exclude(existing, x => x.Id).ToList();

            if (!notExisting.Any())
                return;

            foreach (var aspect in notExisting)
            {
                aspect.CreatedBy = createdBySystem ? _applicationSettings.System : aspect.CreatedBy;
                await _attributeRepository.CreateAspect(aspect);
            }
        }

        #endregion Aspect

        #region Condition

        public Task<IEnumerable<AttributeConditionLibCm>> GetConditions()
        {
            var dataSet = _attributeConditionRepository.GetConditions().Where(x => x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeConditionLibDm>();
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeConditionLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        #endregion Aspect

        #region Format

        public Task<IEnumerable<AttributeFormatLibCm>> GetFormats()
        {
            var dataSet = _attributeFormatRepository.GetFormats().Where(x => x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeFormatLibDm>();
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeFormatLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        #endregion Format

        #region Qualifier

        public Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers()
        {
            var dataSet = _attributeQualifierRepository.GetQualifiers().Where(x => x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeQualifierLibDm>();
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeQualifierLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        #endregion Qualifier

        #region Source

        public Task<IEnumerable<AttributeSourceLibCm>> GetSources()
        {
            var dataSet = _attributeSourceRepository.GetSources().Where(x => x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeSourceLibDm>();
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeSourceLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        #endregion Source
    }
}