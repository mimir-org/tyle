using System.Collections.Generic;
using System.Threading.Tasks;

using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<TerminalDm> GetTerminals();
        List<TerminalDm> GetTerminalsByCategory();
        Task<TerminalDm> CreateTerminalType(TerminalAm terminalAm);
        Task<List<TerminalDm>> CreateTerminalTypes(List<TerminalAm> terminalAmList);
    }
}
