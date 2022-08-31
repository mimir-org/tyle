using System.Collections.Generic;
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
    }
}