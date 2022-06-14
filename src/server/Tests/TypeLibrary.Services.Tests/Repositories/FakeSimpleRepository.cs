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
    public class FakeSimpleRepository : IEfSimpleRepository
    {
        public TypeLibraryDbContext? Context { get; set; }
        public DbSet<SimpleLibDm>? DbSet { get; set; }
        public IQueryable<SimpleLibDm> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SimpleLibDm> FindBy(Expression<Func<SimpleLibDm, bool>> predicate, bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleLibDm> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleLibDm> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<SimpleLibDm>> CreateAsync(SimpleLibDm entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SimpleLibDm entity)
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

        public void Detach(SimpleLibDm entity)
        {
            throw new NotImplementedException();
        }

        public void Detach(ICollection<SimpleLibDm> entities)
        {
            throw new NotImplementedException();
        }

        public void Attach(SimpleLibDm entity, EntityState state)
        {
            throw new NotImplementedException();
        }

        public void Attach(ICollection<SimpleLibDm> entities, EntityState state)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<SimpleLibDm> GetAllSimple()
        {
            throw new NotImplementedException();
        }

        public IQueryable<SimpleLibDm> FindSimple(string id)
        {
            throw new NotImplementedException();
        }
    }
}
