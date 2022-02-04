using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<TerminalLibDm> GetTerminals();
        List<TerminalLibDm> GetTerminalsByCategory();
        Task<TerminalLibDm> CreateTerminalType(TerminalLibAm terminalAm);
        Task<List<TerminalLibDm>> CreateTerminalTypes(List<TerminalLibAm> terminalAmList);
    }
}
