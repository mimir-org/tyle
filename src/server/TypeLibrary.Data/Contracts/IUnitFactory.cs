using System.Collections.Generic;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IUnitFactory
    {
        ICollection<UnitLibDm> AllUnits { get; }
        UnitLibDm Get(string id);
    }
}