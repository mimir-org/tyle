using System.Collections.Generic;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Factories
{
    public interface IUnitFactory
    {
        ICollection<UnitLibDm> AllUnits { get; }
        UnitLibDm Get(string id);
    }
}