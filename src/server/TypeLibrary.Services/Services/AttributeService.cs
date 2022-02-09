using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Data;
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
        public IEnumerable<AttributeLibDm> GetAttributes(Aspect aspect)
        {
            var all = _attributeRepository.GetAll()
                .Include(x => x.AttributeQualifier)
                .Include(x => x.AttributeSource)
                .Include(x => x.AttributeCondition)
                .Include(x => x.AttributeFormat)
                .Include(x => x.Units)
                .ToList();

            return aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect)).ToList();
        }

       /// <summary>
       /// Get all attributes
       /// </summary>
       /// <returns></returns>
        public IEnumerable<AttributeLibDm> GetAttributes()
        {
            var all = _attributeRepository.GetAll()
                .Include(x => x.AttributeQualifier)
                .Include(x => x.AttributeSource)
                .Include(x => x.AttributeCondition)
                .Include(x => x.AttributeFormat)
                .Include(x => x.Units)
                .ToList();

            return all;
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="attributeAm"></param>
        /// <returns></returns>
        public async Task<AttributeLibDm> CreateAttribute(AttributeLibAm attributeAm)
        {
            var data = await CreateAttributes(new List<AttributeLibAm> { attributeAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributeAmList"></param>
        /// <returns></returns>
        public async Task<ICollection<AttributeLibDm>> CreateAttributes(List<AttributeLibAm> attributeAmList)
        {
            if (attributeAmList == null || !attributeAmList.Any())
                return new List<AttributeLibDm>();

            var data = _mapper.Map<List<AttributeLibDm>>(attributeAmList);
            var existing = _attributeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<AttributeLibDm>();

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

            return data;
        }

        /// <summary>
        /// Get predefined attributePredefinedList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributePredefinedLibCm> GetAttributesPredefined()
        {
            var all = _attributePredefinedRepository.GetAll().ToList();
            return _mapper.Map<List<AttributePredefinedLibCm>>(all);
        }

        /// <summary>
        /// Create Predefined attributePredefinedList from a list
        /// </summary>
        /// <param name="attributePredefinedList"></param>
        /// <returns></returns>
        public async Task<List<AttributePredefinedLibDm>> CreateAttributesPredefined(List<AttributePredefinedLibDm> attributePredefinedList)
        {
            if (attributePredefinedList == null || !attributePredefinedList.Any())
                return new List<AttributePredefinedLibDm>();

            var existing = _attributePredefinedRepository.GetAll().ToList();
            var notExisting = attributePredefinedList.Where(x => existing.All(y => y.Key != x.Key)).ToList();

            if (!notExisting.Any())
                return new List<AttributePredefinedLibDm>();

            foreach (var entity in notExisting)
            {
                await _attributePredefinedRepository.CreateAsync(entity);
            }
            await _attributePredefinedRepository.SaveAsync();
            return attributePredefinedList;
        }
    }
}
