using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IQuantityDatumRepository
{
    /// <summary>
    /// Get all quantity datum range specifying
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    Task<List<QuantityDatumDm>> GetQuantityDatumRangeSpecifying();

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    Task<List<QuantityDatumDm>> GetQuantityDatumSpecifiedScope();

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    Task<List<QuantityDatumDm>> GetQuantityDatumSpecifiedProvenance();

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    Task<List<QuantityDatumDm>> GetQuantityDatumRegularitySpecified();
}