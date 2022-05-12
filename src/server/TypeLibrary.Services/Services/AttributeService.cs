using System;
using System.Collections.Generic;
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

        public AttributeService(IMapper mapper, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get all attributes by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        public IEnumerable<AttributeLibCm> GetAttributes(Aspect aspect)
        {
            var attributes = _attributeRepository.GetAllAttributes().ToList();
            attributes = attributes.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (aspect != Aspect.NotSet)
                attributes = attributes.Where(x => x.Aspect.HasFlag(aspect)).ToList();

            return _mapper.Map<List<AttributeLibCm>>(attributes);
        }

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AttributeLibCm> GetAttribute(string id)
        {
            var attribute = await _attributeRepository.GetAttribute(id);
            return _mapper.Map<AttributeLibCm>(attribute);
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="attributeAm"></param>
        /// <returns></returns>
        public async Task<AttributeLibCm> CreateAttribute(AttributeLibAm attributeAm)
        {
            if (attributeAm == null)
                throw new MimirorgNullReferenceException("Can't create an attribute from null object");

            var attribute = _mapper.Map<AttributeLibDm>(attributeAm);

            if (attribute == null)
                throw new MimirorgMappingException(nameof(AttributeLibAm), nameof(AttributeLibDm));

            await _attributeRepository.CreateAttribute(attribute);
            var cm = _mapper.Map<AttributeLibCm>(attribute);
            
            if (cm == null)
                throw new MimirorgMappingException(nameof(AttributeLibDm), nameof(AttributeLibCm));

            return cm;
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributeAmList"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task CreateAttributes(List<AttributeLibAm> attributeAmList, bool createdBySystem = false)
        {
            if (attributeAmList == null || !attributeAmList.Any())
                return;

            var data = _mapper.Map<List<AttributeLibDm>>(attributeAmList);
            var existing = _attributeRepository.GetAllAttributes().ToList();
            var notExisting = data.Exclude(existing, x => x.Id).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributeRepository.CreateAttribute(attribute);
            }
        }

        /// <summary>
        /// Get predefined attributePredefinedList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined()
        {
            var attributes = _attributeRepository.GetAllAttributePredefine()
                .ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<AttributePredefinedLibCm>>(attributes);
        }

        /// <summary>
        /// Create Predefined attributePredefinedList from a list
        /// </summary>
        /// <param name="attributePredefinedList"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task CreateAttributesPredefined(List<AttributePredefinedLibAm> attributePredefinedList, bool createdBySystem = false)
        {
            if (attributePredefinedList == null || !attributePredefinedList.Any())
                return;

            var data = _mapper.Map<List<AttributePredefinedLibDm>>(attributePredefinedList);
            var existing = _attributeRepository.GetAllAttributePredefine().ToList();
            var notExisting = data.Exclude(existing, x => x.Key).ToList();

            if (!notExisting.Any())
                return;

            foreach (var attribute in notExisting)
            {
                attribute.CreatedBy = createdBySystem ? _applicationSettings.System : attribute.CreatedBy;
                await _attributeRepository.CreateAttributePredefined(attribute);
            }
        }
    }
}
