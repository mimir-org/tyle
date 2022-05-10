using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly IAttributePredefinedRepository _attributePredefinedRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IUnitRepository _unitRepository;

        public AttributeService(IMapper mapper, IAttributePredefinedRepository attributePredefinedRepository,
            IAttributeRepository attributeRepository, IUnitRepository unitRepository)
        {
            _mapper = mapper;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeRepository = attributeRepository;
            _unitRepository = unitRepository;
        }

        /// <summary>
        /// Get all attribute files by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        public IEnumerable<AttributeLibCm> GetAttributes(Aspect aspect)
        {
            if (aspect == Aspect.NotSet)
                return GetAttributes();

            var attributes = _attributeRepository.GetAll()
                .Include(x => x.Units).ToList()
                .Where(x => x.Aspect.HasFlag(aspect))
                .OrderBy(x => x.Name).ToList();

            return _mapper.Map<List<AttributeLibCm>>(attributes).ToList();
        }

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributeLibCm> GetAttributes()
        {
            var attributes = _attributeRepository.GetAll()
                .Include(x => x.Units).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<AttributeLibCm>>(attributes).ToList();
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

            _unitRepository.Attach(attribute.Units, EntityState.Unchanged);
            await _attributeRepository.CreateAsync(attribute);
            await _attributeRepository.SaveAsync();
            _unitRepository.Detach(attribute.Units);
            _attributeRepository.Detach(attribute);
            var cm = _mapper.Map<AttributeLibCm>(attribute);
            if (cm == null)
                throw new MimirorgMappingException(nameof(AttributeLibDm), nameof(AttributeLibCm));

            return cm;
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributeAmList"></param>
        /// <returns></returns>
        public async Task CreateAttributes(List<AttributeLibAm> attributeAmList)
        {
            if (attributeAmList == null || !attributeAmList.Any())
                return;

            var data = _mapper.Map<List<AttributeLibDm>>(attributeAmList);
            var existing = _attributeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var entity in notExisting)
            {
                foreach (var entityUnit in entity.Units)
                {
                    _unitRepository.Attach(entityUnit, EntityState.Unchanged);
                }

                await _attributeRepository.CreateAsync(entity);
                await _attributeRepository.SaveAsync();

                foreach (var entityUnit in entity.Units)
                {
                    _unitRepository.Detach(entityUnit);
                }
            }

            foreach (var notExistingItem in notExisting)
            {
                _attributeRepository.Detach(notExistingItem);
            }
        }

        /// <summary>
        /// Get predefined attributePredefinedList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined()
        {
            var all = _attributePredefinedRepository.GetAll().ToList().OrderBy(x => x.Aspect).ThenBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase).ToList();
            return _mapper.Map<List<AttributePredefinedLibCm>>(all);
        }

        /// <summary>
        /// Create Predefined attributePredefinedList from a list
        /// </summary>
        /// <param name="attributePredefinedList"></param>
        /// <returns></returns>
        public async Task CreateAttributesPredefined(List<AttributePredefinedLibAm> attributePredefinedList)
        {
            if (attributePredefinedList == null || !attributePredefinedList.Any())
                return;

            var existing = _attributePredefinedRepository.GetAll().ToList();
            var notExisting = attributePredefinedList.Where(x => existing.All(y => y.Key != x.Key)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var entity in notExisting)
            {
                var dm = _mapper.Map<AttributePredefinedLibDm>(entity);
                await _attributePredefinedRepository.CreateAsync(dm);
            }
            await _attributePredefinedRepository.SaveAsync();
        }
    }
}
