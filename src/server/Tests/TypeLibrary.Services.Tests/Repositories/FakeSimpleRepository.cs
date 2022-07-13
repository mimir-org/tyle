using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public class FakeSimpleRepository : ISimpleRepository
    {
        public Task<SimpleLibDm> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SimpleLibDm> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task Create(SimpleLibDm simple)
        {
            throw new System.NotImplementedException();
        }

        public void ClearAllChangeTrackers()
        {
            throw new System.NotImplementedException();
        }

        public void SetUnchanged(ICollection<SimpleLibDm> items)
        {
            throw new System.NotImplementedException();
        }

        public void SetDetached(ICollection<SimpleLibDm> items)
        {
            throw new System.NotImplementedException();
        }
    }
}