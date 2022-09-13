using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsRepository
    {
        IEnumerable<RdsLibDm> Get();
        Task<RdsLibDm> Create(RdsLibDm rds, State state);
        Task Create(List<RdsLibDm> items, State state);
        void ClearAllChangeTrackers();
    }
}