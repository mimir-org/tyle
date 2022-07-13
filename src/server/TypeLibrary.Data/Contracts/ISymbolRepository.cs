using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ISymbolRepository
    {
        IEnumerable<SymbolLibDm> Get();
        Task Create(List<SymbolLibDm> symbols);
        Task<SymbolLibDm> Create(SymbolLibDm symbol);
        void ClearAllChangeTrackers();
        void SetUnchanged(ICollection<SymbolLibDm> items);
        void SetDetached(ICollection<SymbolLibDm> items);
    }
}