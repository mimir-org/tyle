using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Data.Contracts;

public interface IUnitReferenceRepository
{
    Task<List<UnitLibAm>> FetchUnitsFromReference();
}