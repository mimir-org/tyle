using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
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
        private readonly ApplicationSettings _applicationSettings;
        private readonly IAttributePredefinedRepository _attributePredefinedRepository;
        private readonly IQuantityDatumRepository _datumRepository;
        private readonly IAttributeReferenceRepository _attributeReferenceRepository;

        public AttributeService(IMapper mapper, IOptions<ApplicationSettings> applicationSettings, IAttributePredefinedRepository attributePredefinedRepository, IAttributeReferenceRepository attributeReferenceRepository, IQuantityDatumRepository datumRepository)
        {
            _mapper = mapper;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeReferenceRepository = attributeReferenceRepository;
            _datumRepository = datumRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get all attributes and their units
        /// </summary>
        /// <returns>List of attributes and their units></returns>
        public async Task<ICollection<AttributeLibCm>> Get()
        {
            var dataSet = await _attributeReferenceRepository.Get();
            return _mapper.Map<List<AttributeLibCm>>(dataSet);
        }

        /// <summary>
        /// Get predefined attributes
        /// </summary>
        /// <returns>List of predefined attributes</returns>
        public IEnumerable<AttributePredefinedLibCm> GetPredefined()
        {
            var attributes = _attributePredefinedRepository.GetPredefined().Where(x => x.State != State.Deleted).ToList()
                .OrderBy(x => x.Aspect).ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<AttributePredefinedLibCm>>(attributes);
        }

        /// <summary>
        /// Create predefined attributes
        /// </summary>
        /// <param name="predefined"></param>
        /// <returns>Created predefined attribute</returns>
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

        /// <summary>
        /// Get all quantity datum range specifying
        /// </summary>
        /// <returns>List of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRangeSpecifying()
        {
            var dataSet = await _datumRepository.GetQuantityDatumRangeSpecifying();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum specified scopes
        /// </summary>
        /// <returns>List of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedScope()
        {
            var dataSet = await _datumRepository.GetQuantityDatumSpecifiedScope();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum with specified provenances
        /// </summary>
        /// <returns>List of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumSpecifiedProvenance()
        {
            var dataSet = await _datumRepository.GetQuantityDatumSpecifiedProvenance();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }

        /// <summary>
        /// Get all quantity datum regularity specified
        /// </summary>
        /// <returns>List of quantity datums</returns>
        public async Task<IEnumerable<QuantityDatumCm>> GetQuantityDatumRegularitySpecified()
        {
            var dataSet = await _datumRepository.GetQuantityDatumRegularitySpecified();
            var dataCmList = _mapper.Map<List<QuantityDatumCm>>(dataSet);
            return dataCmList.AsEnumerable();
        }
    }
}