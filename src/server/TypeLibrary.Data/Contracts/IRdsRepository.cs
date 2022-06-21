using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IRdsRepository
    {
        IEnumerable<RdsLibDm> Get();
        Task Create(List<RdsLibDm> dataDm);
        void ClearAllChangeTrackers();
    }
}