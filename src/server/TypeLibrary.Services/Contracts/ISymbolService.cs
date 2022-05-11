using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ISymbolService
    {
        Task<SymbolLibCm> CreateSymbol(SymbolLibAm symbolLibAm, bool saveData = true);
        Task<IEnumerable<SymbolLibCm>> CreateSymbol(IEnumerable<SymbolLibAm> symbolDataList, bool createdBySystem = false);
        Task<SymbolLibCm> UpdateSymbol(string id, SymbolLibAm symbolLibAm);
        IEnumerable<SymbolLibCm> GetSymbol();
    }
}
