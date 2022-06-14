using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface ITerminalRepository
    {
        IEnumerable<TerminalLibDm> Get();
        Task Create(List<TerminalLibDm> dataDm);
        IEnumerable<TerminalLibDm> GetVersions(string firstVersionId);
        void ClearAllChangeTrackers();
    }
}