using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfTerminalRepository : GenericRepository<TypeLibraryDbContext, TerminalLibDm>, IEfTerminalRepository
    {
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfTerminalRepository(TypeLibraryDbContext dbContext, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _typeLibraryProcRepository = typeLibraryProcRepository;
        }

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The terminal id</param>
        /// <returns>The company id of given terminal</returns>
        public async Task<int> HasCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Terminal"},
                {"@Id", id}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
            return result?.FirstOrDefault()?.CompanyId ?? 0;
        }

        /// <summary>
        /// Change the state of the terminal on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of terminal id's</param>
        /// <returns>The number of terminals in given state</returns>
        public async Task<int> ChangeState(State state, ICollection<string> ids)
        {
            if (ids == null)
                return 0;

            var idList = ids.ConvertToString();

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Terminal"},
                {"@State", state.ToString()},
                {"@IdList", idList}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Change all parent id's on terminals from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old terminal parent id</param>
        /// <param name="newId">New terminal parent id</param>
        /// <returns>The number of terminal with the new parent id</returns>
        public async Task<int> ChangeParentId(string oldId, string newId)
        {
            if (string.IsNullOrWhiteSpace(oldId) || string.IsNullOrWhiteSpace(newId))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Terminal"},
                {"@OldId", oldId},
                {"@NewId", newId}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Check if terminal exists
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <returns>True if terminal exist</returns>
        public async Task<bool> Exist(string id)
        {
            return await Exist(x => x.Id == id);
        }

        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns>A collection of terminals</returns>
        public IEnumerable<TerminalLibDm> Get()
        {
            return GetAll().Include(x => x.Attributes);
        }

        /// <summary>
        /// Get terminal by id
        /// </summary>
        /// <param name="id">The terminal id</param>
        /// <returns>Terminal if found</returns>
        public async Task<TerminalLibDm> Get(string id)
        {
            var terminal = await FindBy(x => x.Id == id).Include(x => x.Attributes).FirstOrDefaultAsync();
            return terminal;
        }

        /// <summary>
        /// Create a terminal in database
        /// </summary>
        /// <param name="terminal">The terminal to be created</param>
        /// <returns>The created terminal</returns>
        public async Task<TerminalLibDm> Create(TerminalLibDm terminal)
        {
            await CreateAsync(terminal);
            await SaveAsync();
            return terminal;
        }

        /// <summary>
        /// Clear all entity framework change trackers
        /// </summary>
        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }
    }
}