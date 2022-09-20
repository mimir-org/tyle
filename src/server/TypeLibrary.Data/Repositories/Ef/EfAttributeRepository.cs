using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
    {
        private readonly ApplicationSettings _applicationSettings;

        public EfAttributeRepository(TypeLibraryDbContext dbContext, IOptions<ApplicationSettings> applicationSettings) : base(dbContext)
        {
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns>A collection of attributes</returns>
        /// <remarks>Only attributes that is not deleted will be returned</remarks>
        public IEnumerable<AttributeLibDm> Get()
        {
            return GetAll();
        }

        /// <summary>
        /// Get attribute by id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>If exist it returns the attribute, otherwise it returns null</returns>
        public async Task<AttributeLibDm> Get(string id)
        {
            var item = await FindBy(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        /// <summary>
        /// Update state on an attribute
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        /// <exception cref="MimirorgBadRequestException"></exception>
        public async Task UpdateState(string id, State state)
        {
            var dm = await FindBy(x => x.Id == id).FirstOrDefaultAsync();

            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            if (dm.State == state)
                throw new MimirorgBadRequestException($"Not allowed. Same state. Current state is {dm.State} and new state is {state}");

            dm.State = state;
            Context.Entry(dm).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <param name="state"></param>
        /// <returns>An attribute</returns>
        public async Task<AttributeLibDm> Create(AttributeLibDm attribute, State state)
        {
            attribute.State = state;
            await CreateAsync(attribute);
            await SaveAsync();
            Detach(attribute);
            return attribute;
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The Attribute with id {id} is created by the system and can not be deleted.");

            dm.State = State.Deleted;
            Context.Entry(dm).State = EntityState.Modified;
            return await Context.SaveChangesAsync() == 1;
        }

        /// <summary>
        /// Check if an attribute already exist
        /// </summary>
        /// <param name="id">The attribute id to check if exist</param>
        /// <returns>True if attribute already exist</returns>
        public async Task<bool> Exist(string id)
        {
            var attribute = await FindBy(x => x.Id == id).FirstOrDefaultAsync();
            return attribute != null;
        }

        public void SetUnchanged(ICollection<AttributeLibDm> items)
        {
            Attach(items, EntityState.Unchanged);
        }

        public void SetDetached(ICollection<AttributeLibDm> items)
        {
            Detach(items);
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}