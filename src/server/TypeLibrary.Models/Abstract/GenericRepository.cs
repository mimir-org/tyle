using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TypeLibrary.Models.Abstract
{
    public abstract class GenericRepository<TContext, TEntity> : IGenericRepository<TContext, TEntity> where TContext : DbContext where TEntity : class
    {
        public TContext Context { get; set; }
        public DbSet<TEntity> DbSet { get; set; }

        protected GenericRepository(TContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll(bool noTracking = true)
        {
            return noTracking
                ? DbSet.AsNoTracking()
                : DbSet.AsTracking();
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
        {
            return noTracking
                ? DbSet.AsNoTracking().Where(predicate)
                : DbSet.AsTracking().Where(predicate);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<EntityEntry<TEntity>> CreateAsync(TEntity entity)
        {
            return await DbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(int id)
        {
            var entityToDelete = await DbSet.FindAsync(id);
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        public virtual async Task Delete(string id)
        {
            var entityToDelete = await DbSet.FindAsync(id);
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        public virtual void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public void Attach(TEntity entity, EntityState state)
        {
            Context.Entry(entity).State = state;
        }

        public void Attach(ICollection<TEntity> entities, EntityState state)
        {
            if(entities == null || !entities.Any())
                return;

            foreach (var entity in entities)
            {
                Attach(entity, state);
            }
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
