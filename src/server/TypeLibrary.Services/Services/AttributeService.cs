using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;
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
        public IEnumerable<AttributeDm> GetAttributes(Aspect aspect)
        {
            var all = _attributeRepository.GetAll()
                .Include(x => x.QualifierDm)
                .Include(x => x.SourceDm)
                .Include(x => x.ConditionDm)
                .Include(x => x.FormatDm)
                .Include(x => x.Units)
                .ToList();

            return aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect)).ToList();
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="attributeAm"></param>
        /// <returns></returns>
        public async Task<AttributeDm> CreateAttribute(AttributeAm attributeAm)
        {
            var data = await CreateAttributes(new List<AttributeAm> { attributeAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributeAmList"></param>
        /// <returns></returns>
        public async Task<ICollection<AttributeDm>> CreateAttributes(List<AttributeAm> attributeAmList)
        {
            if (attributeAmList == null || !attributeAmList.Any())
                return new List<AttributeDm>();

            var data = _mapper.Map<List<AttributeDm>>(attributeAmList);
            var existing = _attributeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<AttributeDm>();

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
        public IEnumerable<AttributePredefinedCm> GetAttributesPredefined()
        {
            var all = _attributePredefinedRepository.GetAll().ToList();
            return _mapper.Map<List<AttributePredefinedCm>>(all);
        }

        /// <summary>
        /// Create Predefined attributePredefinedList from a list
        /// </summary>
        /// <param name="attributePredefinedList"></param>
        /// <returns></returns>
        public async Task<List<AttributePredefinedDm>> CreateAttributesPredefined(List<AttributePredefinedDm> attributePredefinedList)
        {
            if (attributePredefinedList == null || !attributePredefinedList.Any())
                return new List<AttributePredefinedDm>();

            var existing = _attributePredefinedRepository.GetAll().ToList();
            var notExisting = attributePredefinedList.Where(x => existing.All(y => y.Key != x.Key)).ToList();

            if (!notExisting.Any())
                return new List<AttributePredefinedDm>();

            foreach (var entity in notExisting)
            {
                await _attributePredefinedRepository.CreateAsync(entity);
            }
            await _attributePredefinedRepository.SaveAsync();
            return attributePredefinedList;
        }
    }
}
