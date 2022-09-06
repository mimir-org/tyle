using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<TerminalLibCm> GetAll(bool includeDeleted = false);
        Task<TerminalLibCm> Get(string id);
        Task Create(List<TerminalLibAm> terminalAmList, bool createdBySystem = false);
        Task<TerminalLibCm> Create(TerminalLibAm terminal);
        Task<TerminalLibCm> Update(TerminalLibAm terminal, string id);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string terminalId, int companyId);
    }
}