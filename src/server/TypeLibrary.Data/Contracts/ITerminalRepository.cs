using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITerminalRepository
    {
        Task<bool> Exist(string id);
        IEnumerable<TerminalLibDm> Get();
        Task<TerminalLibDm> Get(string id);
        Task UpdateState(string id, State state);
        Task Create(List<TerminalLibDm> items, State state);
        Task<TerminalLibDm> Create(TerminalLibDm terminal, State state);
        IEnumerable<TerminalLibDm> GetVersions(string firstVersionId);
        Task<bool> Remove(string id);
        void ClearAllChangeTrackers();
        void SetUnchanged(ICollection<TerminalLibDm> items);
        void SetDetached(ICollection<TerminalLibDm> items);
    }
}