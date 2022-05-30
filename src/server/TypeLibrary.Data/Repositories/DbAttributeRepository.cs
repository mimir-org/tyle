using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbAttributeRepository : IAttributeRepository
    {
        private readonly IEfAttributeRepository _efAttributeRepository;
        private readonly IEfUnitRepository _efUnitRepository;
        private readonly IEfAttributePredefinedRepository _attributePredefinedRepository;
        private readonly IEfAttributeAspectRepository _attributeAspectRepository;
        private readonly IEfAttributeConditionRepository _attributeConditionRepository;

        public DbAttributeRepository(IEfAttributeRepository efAttributeRepository, IEfUnitRepository efUnitRepository, IEfAttributePredefinedRepository attributePredefinedRepository, IEfAttributeAspectRepository attributeAspectRepository, IEfAttributeConditionRepository attributeConditionRepository)
        {
            _efAttributeRepository = efAttributeRepository;
            _efUnitRepository = efUnitRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeAspectRepository = attributeAspectRepository;
            _attributeConditionRepository = attributeConditionRepository;
        }

        #region Attribute

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributeLibDm> Get()
        {
            return _efAttributeRepository.GetAll().Where(x => !x.Deleted).Include(x => x.Units);
        }

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributeLibDm> Create(AttributeLibDm attribute)
        {
            _efUnitRepository.Attach(attribute.Units, EntityState.Unchanged);
            await _efAttributeRepository.CreateAsync(attribute);
            await _efAttributeRepository.SaveAsync();
            _efUnitRepository.Detach(attribute.Units);
            _efAttributeRepository.Detach(attribute);
            return attribute;
        }

        #endregion Attribute
        
        #region Predefined

        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributePredefinedLibDm> GetPredefined()
        {
            return _attributePredefinedRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="predefined">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributePredefinedLibDm> CreatePredefined(AttributePredefinedLibDm predefined)
        {
            await _attributePredefinedRepository.CreateAsync(predefined);
            await _attributePredefinedRepository.SaveAsync();
            return predefined;
        }

        #endregion Predefined

        #region Aspect

        /// <summary>
        /// Get all aspect attributes
        /// </summary>
        /// <returns>A collection of aspect attributes</returns>
        /// <remarks>Only aspect attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributeAspectLibDm> GetAspects()
        {
            return _attributeAspectRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new aspect attribute
        /// </summary>
        /// <param name="aspect">The aspect attribute that should be created</param>
        /// <returns>An aspect attribute</returns>
        public async Task<AttributeAspectLibDm> CreateAspect(AttributeAspectLibDm aspect)
        {
            await _attributeAspectRepository.CreateAsync(aspect);
            await _attributeAspectRepository.SaveAsync();
            return aspect;
        }

        #endregion Aspect

        #region Condition

        /// <summary>
        /// Get all condition attributes
        /// </summary>
        /// <returns>A collection of attribute conditions</returns>
        /// <remarks>Only attribute conditions that is not deleted will be returned</remarks>
        public IEnumerable<AttributeConditionLibDm> GetConditions()
        {
            return _attributeConditionRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new aspect attribute
        /// </summary>
        /// <param name="condition">The aspect attribute that should be created</param>
        /// <returns>An aspect attribute</returns>
        public async Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm condition)
        {
            await _attributeConditionRepository.CreateAsync(condition);
            await _attributeConditionRepository.SaveAsync();
            return condition;
        }

        #endregion Condition
    }
}
