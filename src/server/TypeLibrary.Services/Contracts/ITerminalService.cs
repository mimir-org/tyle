using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITerminalService
    {
        Task<TerminalLibCm> Get(string id);
        Task<IEnumerable<TerminalLibCm>> GetAll(bool includeDeleted = false);
        IEnumerable<TerminalLibCm> GetLatestVersions();
        Task Create(List<TerminalLibAm> terminalAmList, bool createdBySystem = false);
        Task<TerminalLibCm> Create(TerminalLibAm terminal, bool resetVersion);
        Task<TerminalLibCm> Update(TerminalLibAm terminal, string id);
        Task<TerminalLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string terminalId, int companyId);
    }
}