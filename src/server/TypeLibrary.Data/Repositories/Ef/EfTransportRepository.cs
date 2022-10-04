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
    public class EfTransportRepository : GenericRepository<TypeLibraryDbContext, TransportLibDm>, IEfTransportRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfTransportRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _typeLibraryProcRepository = typeLibraryProcRepository;
        }

        /// <summary>
        /// Get the registered company on given id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>The company id of given transport</returns>
        public async Task<int> HasCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Transport"},
                {"@Id", id}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlCompanyId>("HasCompany", procParams);
            return result?.FirstOrDefault()?.CompanyId ?? 0;
        }

        /// <summary>
        /// Change the state of the transport on all listed id's
        /// </summary>
        /// <param name="state">The state to change to</param>
        /// <param name="ids">A list of transport id's</param>
        /// <returns>The number of transports in given state</returns>
        public async Task<int> ChangeState(State state, ICollection<string> ids)
        {
            if (ids == null)
                return 0;

            var idList = ids.ConvertToString();

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Transport"},
                {"@State", state.ToString()},
                {"@IdList", idList}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateState", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Change all parent id's on transports from old id to the new id 
        /// </summary>
        /// <param name="oldId">Old transport parent id</param>
        /// <param name="newId">New transport parent id</param>
        /// <returns>The number of transports with the new parent id</returns>
        public async Task<int> ChangeParentId(string oldId, string newId)
        {
            if (string.IsNullOrWhiteSpace(oldId) || string.IsNullOrWhiteSpace(newId))
                return 0;

            var procParams = new Dictionary<string, object>
            {
                {"@TableName", "Transport"},
                {"@OldId", oldId},
                {"@NewId", newId}
            };

            var result = await _typeLibraryProcRepository.ExecuteStoredProc<SqlResultCount>("UpdateParentId", procParams);
            return result?.FirstOrDefault()?.Number ?? 0;
        }

        /// <summary>
        /// Check if transport exists
        /// </summary>
        /// <param name="id">The id of the transport</param>
        /// <returns>True if transport exist</returns>
        public async Task<bool> Exist(string id)
        {
            return await Exist(x => x.Id == id);
        }

        /// <summary>
        /// Get all transports
        /// </summary>
        /// <returns>A collection of transports</returns>
        public IEnumerable<TransportLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .OrderBy(x => x.Name)
                .AsSplitQuery();
        }

        /// <summary>
        /// Get transport by id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>Transport if found</returns>
        public async Task<TransportLibDm> Get(string id)
        {
            var item = await FindBy(x => x.Id == id)
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            return item;
        }

        /// <summary>
        /// Create a transport
        /// </summary>
        /// <param name="transportDm">The transport to be created</param>
        /// <returns>The created transport</returns>
        public async Task<TransportLibDm> Create(TransportLibDm transportDm)
        {
            if (transportDm.Attributes != null && transportDm.Attributes.Any())
                _attributeRepository.SetUnchanged(transportDm.Attributes);

            await CreateAsync(transportDm);
            await SaveAsync();

            if (transportDm.Attributes != null && transportDm.Attributes.Any())
                _attributeRepository.SetDetached(transportDm.Attributes);

            Detach(transportDm);

            return transportDm;
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
            _attributeRepository.ClearAllChangeTrackers();
        }
    }
}