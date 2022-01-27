using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using PredefinedAttribute = TypeLibrary.Models.Application.PredefinedAttribute;

namespace TypeLibrary.Services.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly IPredefinedAttributeRepository _predefinedAttributeRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IUnitRepository _unitRepository;

        public AttributeService(IMapper mapper, IPredefinedAttributeRepository predefinedAttributeRepository, 
            IAttributeRepository attributeRepository, IUnitRepository unitRepository)
        {
            _mapper = mapper;
            _predefinedAttributeRepository = predefinedAttributeRepository;
            _attributeRepository = attributeRepository;
            _unitRepository = unitRepository;
        }

        /// <summary>
        /// Get all attribute files by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        public IEnumerable<Attribute> GetAttributes(Aspect aspect)
        {
            var all = _attributeRepository.GetAll()
                .Include(x => x.Qualifier)
                .Include(x => x.Source)
                .Include(x => x.Condition)
                .Include(x => x.Format)
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
        public async Task<Attribute> CreateAttribute(AttributeAm attributeAm)
        {
            var data = await CreateAttributes(new List<AttributeAm> { attributeAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of attributes
        /// </summary>
        /// <param name="attributeAmList"></param>
        /// <returns></returns>
        public async Task<ICollection<Attribute>> CreateAttributes(List<AttributeAm> attributeAmList)
        {
            if (attributeAmList == null || !attributeAmList.Any())
                return new List<Attribute>();

            var data = _mapper.Map<List<Attribute>>(attributeAmList);
            var existing = _attributeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<Attribute>();

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
        /// Get predefined predefinedAttributeList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PredefinedAttribute> GetPredefinedAttributes()
        {
            var all = _predefinedAttributeRepository.GetAll().ToList();
            return _mapper.Map<List<PredefinedAttribute>>(all);
        }

        /// <summary>
        /// Create Predefined predefinedAttributeList from a list
        /// </summary>
        /// <param name="predefinedAttributeList"></param>
        /// <returns></returns>
        public async Task<List<Models.Data.PredefinedAttribute>> CreatePredefinedAttributes(List<Models.Data.PredefinedAttribute> predefinedAttributeList)
        {
            if (predefinedAttributeList == null || !predefinedAttributeList.Any())
                return new List<Models.Data.PredefinedAttribute>();

            var existing = _predefinedAttributeRepository.GetAll().ToList();
            var notExisting = predefinedAttributeList.Where(x => existing.All(y => y.Key != x.Key)).ToList();

            if (!notExisting.Any())
                return new List<Models.Data.PredefinedAttribute>();

            foreach (var entity in notExisting)
            {
                await _predefinedAttributeRepository.CreateAsync(entity);
            }
            await _predefinedAttributeRepository.SaveAsync();
            return predefinedAttributeList;
        }
    }
}
