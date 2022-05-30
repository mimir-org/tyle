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
        /// <param name="condition">The attribute condition that should be created</param>
        /// <returns>An attribute condition</returns>
        Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm condition);

        #endregion Condition
    }
}