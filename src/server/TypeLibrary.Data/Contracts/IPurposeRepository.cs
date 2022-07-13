using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IPurposeRepository
    {
        IEnumerable<PurposeLibDm> Get();
        Task Create(List<PurposeLibDm> dataDm);
        void SetAdded(ICollection<PurposeLibDm> items);
        void SetDetached(ICollection<PurposeLibDm> items);
        void ClearAllChangeTrackers();
    }
}