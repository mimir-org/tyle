using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITerminalRepository
    {
        IEnumerable<TerminalLibDm> Get();
        Task<TerminalLibDm> Get(string id);
        Task Create(List<TerminalLibDm> items);
        Task<TerminalLibDm> Create(TerminalLibDm terminal);
        IEnumerable<TerminalLibDm> GetVersions(string firstVersionId);
        void ClearAllChangeTrackers();
        void SetUnchanged(ICollection<TerminalLibDm> items);
        void SetDetached(ICollection<TerminalLibDm> items);
    }
}