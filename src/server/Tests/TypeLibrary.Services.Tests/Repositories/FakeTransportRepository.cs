using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Tests.Repositories
{
    public class FakeTransportRepository : IEfTransportRepository
    {
        public TypeLibraryDbContext? Context { get; set; }
        public DbSet<TransportLibDm>? DbSet { get; set; }
        public IQueryable<TransportLibDm> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TransportLibDm> FindBy(Expression<Func<TransportLibDm, bool>> predicate, bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<TransportLibDm> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TransportLibDm> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<TransportLibDm>> CreateAsync(TransportLibDm entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TransportLibDm entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Detach(TransportLibDm entity)
        {
            throw new NotImplementedException();
        }

        public void Detach(ICollection<TransportLibDm> entities)
        {
            throw new NotImplementedException();
        }

        public void Attach(TransportLibDm entity, EntityState state)
        {
            throw new NotImplementedException();
        }

        public void Attach(ICollection<TransportLibDm> entities, EntityState state)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TransportLibDm> GetAllTransports()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TransportLibDm> FindTransport(string id)
        {
            throw new NotImplementedException();
        }
    }
}
