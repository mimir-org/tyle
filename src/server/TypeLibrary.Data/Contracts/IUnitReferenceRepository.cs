using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IUnitReferenceRepository
{
    Task<List<UnitLibDm>> FetchUnitsFromReference();
}