using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts
{
    public interface IPurposeRepository
    {
        IEnumerable<PurposeLibDm> Get();
        Task Create(List<PurposeLibDm> dataDm, State state);
        void SetAdded(ICollection<PurposeLibDm> items);
        void SetDetached(ICollection<PurposeLibDm> items);
        void ClearAllChangeTrackers();
    }
}