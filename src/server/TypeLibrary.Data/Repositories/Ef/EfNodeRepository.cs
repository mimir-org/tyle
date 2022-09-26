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
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfNodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, IEfNodeRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ApplicationSettings _applicationSettings;
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfNodeRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _typeLibraryProcRepository = typeLibraryProcRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The node id</param>
        /// <returns>The company id of given terminal</returns>
        public async Task<int> HasCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Node"},
                {"@Id", id}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
            return result?.FirstOrDefault()?.CompanyId ?? 0;
        }

        /// <summary>
        /// Change the state of the node on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of node id's</param>
        /// <returns>The number of nodes in given state</returns>
        public async Task<int> ChangeState(State state, ICollection<string> ids)
        {
            if (ids == null)
                return 0;

            var idList = ids.ConvertToString();

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Node"},
                {"@State", state.ToString()},
                {"@IdList", idList}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Change all parent id's on nodes from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old node parent id</param>
        /// <param name="newId">New node parent id</param>
        /// <returns>The number of nodes with the new parent id</returns>
        public async Task<int> ChangeParentId(string oldId, string newId)
        {
            if (string.IsNullOrWhiteSpace(oldId) || string.IsNullOrWhiteSpace(newId))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Node"},
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
        /// Get all nodes
        /// </summary>
        /// <returns>A collection of nodes</returns>
        public IEnumerable<NodeLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Attributes)
                .Include(x => x.NodeTerminals)
                    .ThenInclude(x => x.Terminal)
                    .ThenInclude(x => x.Attributes)
                .AsSplitQuery();
        }

        /// <summary>
        /// Get node by id
        /// </summary>
        /// <param name="id">The node id</param>
        /// <returns>Node if found</returns>
        public async Task<NodeLibDm> Get(string id)
        {
            return await FindBy(x => x.Id == id)
                .Include(x => x.Attributes)
                .Include(x => x.NodeTerminals)
                    .ThenInclude(x => x.Terminal)
                    .ThenInclude(x => x.Attributes)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create a node
        /// </summary>
        /// <param name="node">The node to be created</param>
        /// <returns>The created node</returns>
        public async Task<NodeLibDm> Create(NodeLibDm node)
        {
            _attributeRepository.SetUnchanged(node.Attributes);

            await CreateAsync(node);
            await SaveAsync();

            _attributeRepository.SetDetached(node.Attributes);
            Detach(node);
            return node;
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Node with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The node with id {id} is created by the system and can not be deleted.");

            dm.State = State.Deleted;
            Context.Entry(dm).State = EntityState.Modified;
            return await Context.SaveChangesAsync() == 1;
        }

        /// <summary>
        /// Clear all entity framework change trackers
        /// </summary>
        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }
    }
}