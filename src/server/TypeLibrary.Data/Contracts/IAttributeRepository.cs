using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IAttributeRepository
{
    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    IEnumerable<AttributeLibDm> Get();
}