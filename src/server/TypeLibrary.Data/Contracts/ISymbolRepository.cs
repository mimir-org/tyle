using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ISymbolRepository
    {
        IEnumerable<SymbolLibDm> Get();
        Task Create(List<SymbolLibDm> symbols, State state);
        Task<SymbolLibDm> Create(SymbolLibDm symbol, State state);
        void ClearAllChangeTrackers();
        void SetUnchanged(ICollection<SymbolLibDm> items);
        void SetDetached(ICollection<SymbolLibDm> items);
    }
}