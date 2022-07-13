using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfNodeRepository : GenericRepository<TypeLibraryDbContext, NodeLibDm>, IEfNodeRepository
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleRepository _simpleRepository;
        private readonly ApplicationSettings _applicationSettings;

        public EfNodeRepository(TypeLibraryDbContext dbContext, IAttributeRepository attributeRepository, ISimpleRepository simpleRepository, IOptions<ApplicationSettings> applicationSettings) : base(dbContext)
        {
            _attributeRepository = attributeRepository;
            _simpleRepository = simpleRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<NodeLibDm> Get()
        {
            return GetAll()
                .Include(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .Include(x => x.NodeTerminals)
                    .ThenInclude(x => x.Terminal)
                    .ThenInclude(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .Include(x => x.Simples)
                    .ThenInclude(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .AsSplitQuery();
        }

        public async Task<NodeLibDm> Get(string id)
        {
            return await FindBy(x => x.Id == id)
                .Include(x => x.Attributes)
                .Include("AttributeIdList.Units")
                .Include(x => x.NodeTerminals)
                .Include("TerminalNodes.Terminal")
                .Include("TerminalNodes.Terminal.TerminalCategory")
                .Include("TerminalNodes.Terminal.AttributeIdList")
                .Include("TerminalNodes.Terminal.AttributeIdList.Units")
                .Include(x => x.Simples)
                .Include("SimpleTypes.AttributeIdList")
                .Include("SimpleTypes.AttributeIdList.Units")
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<NodeLibDm> Create(NodeLibDm node)
        {
            _attributeRepository.SetUnchanged(node.Attributes);
            _simpleRepository.SetUnchanged(node.Simples);
            await CreateAsync(node);
            await SaveAsync();

            _simpleRepository.SetDetached(node.Simples);
            _attributeRepository.SetDetached(node.Attributes);
            Detach(node);
            return node;
        }

        public async Task<bool> Remove(string id)
        {
            var dm = await Get(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Transport with id {id} not found, delete failed.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be deleted.");
            
            dm.Deleted = true;

            var status = await Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _simpleRepository.ClearAllChangeTrackers();
            _attributeRepository.ClearAllChangeTrackers();
            Context?.ChangeTracker.Clear();
        }
    }
}