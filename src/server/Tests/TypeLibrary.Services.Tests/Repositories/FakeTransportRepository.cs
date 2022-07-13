using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public class FakeTransportRepository : ITransportRepository
    {
        public IEnumerable<TransportLibDm> Get()
        {
            throw new NotImplementedException();
        }

        public Task<TransportLibDm> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(TransportLibDm dataDm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void ClearAllChangeTrackers()
        {
            throw new NotImplementedException();
        }
    }
}