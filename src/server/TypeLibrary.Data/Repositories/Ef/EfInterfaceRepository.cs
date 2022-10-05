using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.Common;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfInterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceLibDm>, IEfInterfaceRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfInterfaceRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _typeLibraryProcRepository = typeLibraryProcRepository;
        }

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>The company id of given interface</returns>
        public async Task<int> HasCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Interface"},
                {"@Id", id}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
            return result?.FirstOrDefault()?.CompanyId ?? 0;
        }

        /// <summary>
        /// Change the state of the interface on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of interface id's</param>
        /// <returns>The number of interfaces in given state</returns>
        public async Task<int> ChangeState(State state, ICollection<string> ids)
        {
            if (ids == null)
                return 0;

            var idList = ids.ConvertToString();

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Interface"},
                {"@State", state.ToString()},
                {"@IdList", idList}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Change all parent id's on interfaces from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old interface parent id</param>
        /// <param name="newId">New interface parent id</param>
        /// <returns>The number of interfaces with the new parent id</returns>
        public async Task<int> ChangeParentId(string oldId, string newId)
        {
            if (string.IsNullOrWhiteSpace(oldId) || string.IsNullOrWhiteSpace(newId))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Interface"},
                {"@OldId", oldId},
                {"@NewId", newId}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Check if interface exists
        /// </summary>
        /// <param name="id">The id of the interface</param>
        /// <returns>True if interface exist</returns>
        public async Task<bool> Exist(string id)
        {
            return await Exist(x => x.Id == id);
        }

        /// <summary>
        /// Get all interfaces
        /// </summary>
        /// <returns>A collection of interfaces</returns>
        public IEnumerable<InterfaceLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .OrderBy(x => x.Name)
                .AsSplitQuery();
        }

        /// <summary>
        /// Get interface by id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>Interface if found</returns>
        public async Task<InterfaceLibDm> Get(string id)
        {
            var item = await FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        /// <summary>
        /// Create an interface
        /// </summary>
        /// <param name="interfaceDm">The interface to be created</param>
        /// <returns>The created interface</returns>
        public async Task<InterfaceLibDm> Create(InterfaceLibDm interfaceDm)
        {
            if (interfaceDm?.Attributes != null && interfaceDm.Attributes.Any())
                _attributeRepository.SetUnchanged(interfaceDm.Attributes);

            await CreateAsync(interfaceDm);
            await SaveAsync();

            if (interfaceDm?.Attributes != null && interfaceDm.Attributes.Any())
                _attributeRepository.SetDetached(interfaceDm.Attributes);

            Detach(interfaceDm);

            return interfaceDm;
        }

        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }
    }
}