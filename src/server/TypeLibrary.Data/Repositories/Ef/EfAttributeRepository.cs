using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IEfAttributeRepository
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfAttributeRepository(TypeLibraryDbContext dbContext, IOptions<ApplicationSettings> applicationSettings, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _typeLibraryProcRepository = typeLibraryProcRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The attribute id</param>
        /// <returns>The company id of given attribute</returns>
        public async Task<int> HasCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Attribute"},
                {"@Id", id}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
            return result?.FirstOrDefault()?.CompanyId ?? 0;
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
        /// Change the state of the attribute on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of attribute id's</param>
        /// <returns>The number of attributes in given state</returns>
        public async Task<int> ChangeState(State state, ICollection<string> ids)
        {
            if (ids == null)
                return 0;

            var idList = ids.ConvertToString();

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Attribute"},
                {"@State", state.ToString()},
                {"@IdList", idList}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attribute">The attribute that should be created</param>
        /// <returns>An attribute</returns>
        public async Task<AttributeLibDm> Create(AttributeLibDm attribute)
        {
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