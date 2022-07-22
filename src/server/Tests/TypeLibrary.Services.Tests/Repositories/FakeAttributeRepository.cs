using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public sealed class FakeAttributeRepository : IAttributeRepository
    {
        public IEnumerable<AttributeLibDm> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<AttributeLibDm> Create(AttributeLibDm attribute)
        {
            throw new System.NotImplementedException();
        }

        public void SetUnchanged(ICollection<AttributeLibDm> items)
        {
            throw new System.NotImplementedException();
        }

        public void SetDetached(ICollection<AttributeLibDm> items)
        {
            throw new System.NotImplementedException();
        }

        public void ClearAllChangeTrackers()
        {
            throw new System.NotImplementedException();
        }
    }
}