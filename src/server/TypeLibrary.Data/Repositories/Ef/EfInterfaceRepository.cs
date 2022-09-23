using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
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
        private readonly ApplicationSettings _applicationSettings;
        private readonly ITypeLibraryProcRepository _typeLibraryProcRepository;

        public EfInterfaceRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, IOptions<ApplicationSettings> applicationSettings, ITypeLibraryProcRepository typeLibraryProcRepository) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _typeLibraryProcRepository = typeLibraryProcRepository;
            _applicationSettings = applicationSettings?.Value;
        }

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

        public IEnumerable<InterfaceLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Terminal)
                .Include(x => x.Attributes)
                .Include(x => x.Parent)
                .OrderBy(x => x.Name)
                .AsSplitQuery();
        }

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

        public async Task UpdateState(string id, State state)
        {
            var dm = await FindBy(x => x.Id == id).FirstOrDefaultAsync();

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found.");

            if (dm.State == state)
                throw new MimirorgBadRequestException($"Not allowed. Same state. Current state is {dm.State} and new state is {state}");

            dm.State = state;
            Context.Entry(dm).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task Create(InterfaceLibDm dataDm, State state)
        {
            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetUnchanged(dataDm.Attributes);

            if (dataDm != null)
                dataDm.State = state;

            await CreateAsync(dataDm);
            await SaveAsync();

            if (dataDm?.Attributes != null && dataDm.Attributes.Any())
                _attributeRepository.SetDetached(dataDm.Attributes);

            Detach(dataDm);

            ClearAllChangeTrackers();
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be deleted.");

            dm.State = State.Deleted;
            Context.Entry(dm).State = EntityState.Modified;
            return await Context.SaveChangesAsync() == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }
    }
}