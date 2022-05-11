using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<TerminalLibCm> GetTerminals();
        Task<TerminalLibCm> CreateTerminal(TerminalLibAm terminalAm);
        Task<List<TerminalLibCm>> CreateTerminals(List<TerminalLibAm> terminalAmList, bool createdBySystem = false);
    }
}
