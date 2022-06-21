using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeFormatRepository
    {
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
    }
}