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
        private readonly IEfAttributeFormatRepository _attributeFormatRepository;
        private readonly IEfAttributeQualifierRepository _attributeQualifierRepository;
        private readonly IEfAttributeSourceRepository _attributeSourceRepository;

        public DbAttributeRepository(IEfAttributeRepository efAttributeRepository, IEfUnitRepository efUnitRepository, IEfAttributePredefinedRepository attributePredefinedRepository, IEfAttributeAspectRepository attributeAspectRepository, IEfAttributeConditionRepository attributeConditionRepository, IEfAttributeFormatRepository attributeFormatRepository, IEfAttributeQualifierRepository attributeQualifierRepository, IEfAttributeSourceRepository attributeSourceRepository)
        {
            _efAttributeRepository = efAttributeRepository;
            _efUnitRepository = efUnitRepository;
            _attributePredefinedRepository = attributePredefinedRepository;
            _attributeAspectRepository = attributeAspectRepository;
            _attributeConditionRepository = attributeConditionRepository;
            _attributeFormatRepository = attributeFormatRepository;
            _attributeQualifierRepository = attributeQualifierRepository;
            _attributeSourceRepository = attributeSourceRepository;
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
        /// Create a new condition attribute
        /// </summary>
        /// <param name="format">The condition attribute that should be created</param>
        /// <returns>A condition attribute</returns>
        public async Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm format)
        {
            await _attributeConditionRepository.CreateAsync(format);
            await _attributeConditionRepository.SaveAsync();
            return format;
        }

        #endregion Condition

        #region Format

        /// <summary>
        /// Get all format attributes
        /// </summary>
        /// <returns>A collection of attribute formats</returns>
        /// <remarks>Only attribute formats that is not deleted will be returned</remarks>
        public IEnumerable<AttributeFormatLibDm> GetFormats()
        {
            return _attributeFormatRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new format attribute
        /// </summary>
        /// <param name="format">The aspect attribute that should be created</param>
        /// <returns>A format attribute</returns>
        public async Task<AttributeFormatLibDm> CreateFormat(AttributeFormatLibDm format)
        {
            await _attributeFormatRepository.CreateAsync(format);
            await _attributeFormatRepository.SaveAsync();
            return format;
        }

        #endregion Format

        #region Qualifier

        /// <summary>
        /// Get all qualifier attributes
        /// </summary>
        /// <returns>A collection of attribute qualifier</returns>
        /// <remarks>Only attribute qualifiers that is not deleted will be returned</remarks>
        public IEnumerable<AttributeQualifierLibDm> GetQualifiers()
        {
            return _attributeQualifierRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new format attribute
        /// </summary>
        /// <param name="qualifier">The aspect attribute that should be created</param>
        /// <returns>A format attribute</returns>
        public async Task<AttributeQualifierLibDm> CreateQualifier(AttributeQualifierLibDm qualifier)
        {
            await _attributeQualifierRepository.CreateAsync(qualifier);
            await _attributeQualifierRepository.SaveAsync();
            return qualifier;
        }

        #endregion Qualifier

        #region Source

        /// <summary>
        /// Get all qualifier attributes
        /// </summary>
        /// <returns>A collection of attribute qualifier</returns>
        /// <remarks>Only attribute qualifiers that is not deleted will be returned</remarks>
        public IEnumerable<AttributeSourceLibDm> GetSources()
        {
            return _attributeSourceRepository.GetAll().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Create a new source attribute
        /// </summary>
        /// <param name="source">The source attribute that should be created</param>
        /// <returns>A source attribute</returns>
        public async Task<AttributeSourceLibDm> CreateSource(AttributeSourceLibDm source)
        {
            await _attributeSourceRepository.CreateAsync(source);
            await _attributeSourceRepository.SaveAsync();
            return source;
        }

        #endregion Source
    }
}