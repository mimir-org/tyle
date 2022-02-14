using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<TerminalLibDm> GetTerminals();
        Task<TerminalLibDm> CreateTerminal(TerminalLibAm terminalAm);
        Task<List<TerminalLibDm>> CreateTerminals(List<TerminalLibAm> terminalAmList);
    }
}
