using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeRepository
    {

        #region Attribute

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributeLibDm> Get();

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        Task<AttributeLibDm> Create(AttributeLibDm attribute);

        #endregion Attribute

        #region Predefined

        /// <summary>
        /// Get all predefined attributes
        /// </summary>
        /// <returns>A collection of predefined attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributePredefinedLibDm> GetPredefined();

        /// <summary>
        /// Create a predefined attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        Task<AttributePredefinedLibDm> CreatePredefined(AttributePredefinedLibDm attribute);

        #endregion Predefined

        #region Aspect

        /// <summary>
        /// Get all aspect attributes
        /// </summary>
        /// <returns>A collection of aspect attributes</returns>
        /// <remarks>Only aspect attributes that is not deleted will be returned</remarks>
        IEnumerable<AttributeAspectLibDm> GetAspects();

        /// <summary>
        /// Creates a new aspect attribute
        /// </summary>
        /// <param name="aspect">The aspect attribute that should be created</param>
        /// <returns>An aspect attribute</returns>
        Task<AttributeAspectLibDm> CreateAspect(AttributeAspectLibDm aspect);

        #endregion Aspect

        #region Condition

        /// <summary>
        /// Get all attribute conditions
        /// </summary>
        /// <returns>A collection of attribute conditions</returns>
        /// <remarks>Only conditions that is not deleted will be returned</remarks>
        IEnumerable<AttributeConditionLibDm> GetConditions();

        /// <summary>
        /// Creates a new attribute condition
        /// </summary>
        /// <param name="format">The attribute condition that should be created</param>
        /// <returns>An attribute condition</returns>
        Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm format);

        #endregion Condition

        #region Format

        /// <summary>
        /// Get all attribute formats
        /// </summary>
        /// <returns>A collection of attribute formats</returns>
        /// <remarks>Only formats that is not deleted will be returned</remarks>
        IEnumerable<AttributeFormatLibDm> GetFormats();

        /// <summary>
        /// Creates a new attribute format
        /// </summary>
        /// <param name="format">The attribute format that should be created</param>
        /// <returns>An attribute format</returns>
        Task<AttributeFormatLibDm> CreateFormat(AttributeFormatLibDm format);

        #endregion Format

        #region Qualifier

        /// <summary>
        /// Get all attribute qualifiers
        /// </summary>
        /// <returns>A collection of attribute qualifiers</returns>
        /// <remarks>Only qualifiers that is not deleted will be returned</remarks>
        IEnumerable<AttributeQualifierLibDm> GetQualifiers();

        /// <summary>
        /// Creates a new attribute qualifier
        /// </summary>
        /// <param name="qualifier">The attribute qualifier that should be created</param>
        /// <returns>An attribute qualifier</returns>
        Task<AttributeQualifierLibDm> CreateQualifier(AttributeQualifierLibDm qualifier);

        #endregion Qualifier

        #region Source

        /// <summary>
        /// Get all attribute sources
        /// </summary>
        /// <returns>A collection of attribute sources</returns>
        /// <remarks>Only sources that is not deleted will be returned</remarks>
        IEnumerable<AttributeSourceLibDm> GetSources();

        /// <summary>
        /// Creates a new attribute source
        /// </summary>
        /// <param name="source">The attribute qualifier that should be created</param>
        /// <returns>An attribute qualifier</returns>
        Task<AttributeSourceLibDm> CreateSource(AttributeSourceLibDm source);

        #endregion Source
    }
}