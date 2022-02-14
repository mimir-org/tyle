using System.Collections.Generic;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IAttributeFactory
    {
        ICollection<AttributeLibDm> AllAttributes { get; }
        AttributeLibDm Get(string id);
    }
}
