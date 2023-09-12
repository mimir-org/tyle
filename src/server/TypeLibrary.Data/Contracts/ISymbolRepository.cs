using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts;

public interface ISymbolRepository
{
    IEnumerable<SymbolLibDm> Get();
    SymbolLibDm Get(string id);
    Task Create(List<SymbolLibDm> symbols, State state);
    void ClearAllChangeTrackers();
}