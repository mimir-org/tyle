using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
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
        private readonly IAttributeQualifierRepository _attributeQualifierRepository;
        private readonly IAttributeSourceRepository _attributeSourceRepository;
        private readonly IAttributeFormatRepository _attributeFormatRepository;
        private readonly IAttributeConditionRepository _attributeConditionRepository;
        private readonly IAttributePredefinedRepository _attributePredefinedRepository;
        private readonly IAttributeReferenceRepository _attributeReferenceRepository;

        public AttributeService(IMapper mapper, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, IAttributeQualifierRepository attributeQualifierRepository, IAttributeSourceRepository attributeSourceRepository, IAttributeFormatRepository attributeFormatRepository, IAttributeConditionRepository attributeConditionRepository, IAttributePredefinedRepository attributePredefinedRepository, IAttributeReferenceRepository attributeReferenceRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
            _attributeQualifierRepository = attributeQualifierRepository;
            _attributeSourceRepository = attributeSourceRepository;
            _attributeFormatRepository = attributeFormatRepository;
            _attributeConditionRepository = attributeConditionRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeReferenceRepository = attributeReferenceRepository;
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
            var attributes = _attributeRepository.Get().Where(x => x.State != State.Deleted).ToList()
                .OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

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

        public async Task<AttributeLibCm> Create(AttributeLibAm attribute)
        {
            if (attribute == null)
                throw new MimirorgBadRequestException("Can't create an attribute that is null.");

            var status = attribute.ValidateObject();
            if (!status.IsValid)
                throw new MimirorgBadRequestException("The attribute is not valid.", status);

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

        #region Condition

        public async Task<IEnumerable<AttributeConditionLibCm>> GetConditions()
        {
            var dataSet = await _attributeConditionRepository.GetConditions();
            return _mapper.Map<List<AttributeConditionLibCm>>(dataSet);
        }

        #endregion Aspect

        #region Format

        public Task<IEnumerable<AttributeFormatLibCm>> GetFormats()
        {
            var dataSet = _attributeFormatRepository.GetFormats().ToList();

            var dataDmList = new List<AttributeFormatLibDm>();
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeFormatLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        #endregion Format

        #region Qualifier

        public async Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers()
        {
            var dataSet = await _attributeQualifierRepository.GetQualifiers();
            return _mapper.Map<List<AttributeQualifierLibCm>>(dataSet);
        }

        #endregion Qualifier

        #region Source

        public async Task<IEnumerable<AttributeSourceLibCm>> GetSources()
        {
            var dataSet = await _attributeSourceRepository.GetSources();
            return _mapper.Map<List<AttributeSourceLibCm>>(dataSet);
        }

        #endregion Source

        #region AttributeReferences

        public async Task<IEnumerable<TypeReferenceCm>> GetAttributeReferences()
        {
            var dataSet = await _attributeReferenceRepository.Get();
            return _mapper.Map<List<TypeReferenceCm>>(dataSet);
        }

        #endregion AttributeReferences
    }
}