using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IUnitRepository
    {
        IEnumerable<UnitLibDm> Get();
        Task Create(List<UnitLibDm> dataDm);
        void ClearAllChangeTrackers();
        void SetAttachUnchanged(ICollection<UnitLibDm> units);
        void Detach(ICollection<UnitLibDm> units);
    }
}