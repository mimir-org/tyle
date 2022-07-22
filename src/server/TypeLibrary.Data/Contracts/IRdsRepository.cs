using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsRepository
    {
        IEnumerable<RdsLibDm> Get();
        Task<RdsLibDm> Create(RdsLibDm rds);
        Task Create(List<RdsLibDm> items);
        void ClearAllChangeTrackers();
    }
}