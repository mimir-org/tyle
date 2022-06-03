using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ISymbolService
    {
        Task<IEnumerable<SymbolLibCm>> CreateSymbol(IEnumerable<SymbolLibAm> symbolDataList, bool createdBySystem = false);
        IEnumerable<SymbolLibCm> GetSymbol();
    }
}
