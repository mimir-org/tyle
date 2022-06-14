using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ISymbolService
    {
        IEnumerable<SymbolLibCm> Get();
        Task Create(IEnumerable<SymbolLibAm> symbolDataList, bool createdBySystem = false);
    }
}
