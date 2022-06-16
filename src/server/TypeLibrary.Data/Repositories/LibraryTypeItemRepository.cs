using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeItemRepository : ILibraryTypeItemRepository
    {
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfInterfaceRepository _interfaceRepository;
        private readonly IEfNodeRepository _nodeRepository;
        private readonly IMapper _mapper;

        public LibraryTypeItemRepository(IMapper mapper, IEfTransportRepository transportRepository,
            IEfInterfaceRepository interfaceRepository, IEfNodeRepository nodeRepository)
        {
            _mapper = mapper;
            _transportRepository = transportRepository;
            _interfaceRepository = interfaceRepository;
            _nodeRepository = nodeRepository;
        }

        public async Task<IEnumerable<NodeLibCm>> GetNodes(string searchString = null)
        {
            var nodeTypes = await _nodeRepository.GetAll()
                .Include(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .Include(x => x.NodeTerminals)
                    .ThenInclude(x => x.Terminal)
                    .ThenInclude(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .Include(x => x.Simples)
                    .ThenInclude(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .AsSplitQuery()
                .ToArrayAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
                nodeTypes = nodeTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            return nodeTypes.Select(nodeType => _mapper.Map<NodeLibCm>(nodeType)).ToList();
        }

        public async Task<IEnumerable<InterfaceLibCm>> GetInterfaces(string searchString = null)
        {

            var interfaceTypes = await _interfaceRepository.GetAll()
                .Include(x => x.Attributes)
                .OrderBy(x => x.Name)
                .AsSplitQuery()
                .ToArrayAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
                interfaceTypes = interfaceTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            return interfaceTypes.Select(interfaceType => _mapper.Map<InterfaceLibCm>(interfaceType)).ToList();
        }

        public async Task<IEnumerable<TransportLibCm>> GetTransports(string searchString = null)
        {
            var transportTypes = await _transportRepository.GetAll()
                .Include(x => x.Attributes)
                .OrderBy(x => x.Name)
                .AsSplitQuery()
                .ToArrayAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
                transportTypes = transportTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            return transportTypes.Select(transportType => _mapper.Map<TransportLibCm>(transportType)).ToList();
        }

        public async Task<T> GetLibraryItem<T>(string id) where T : class, new()
        {
            if (typeof(NodeLibCm).IsAssignableFrom(typeof(T)))
            {
                var nodeType = await _nodeRepository.FindBy(x => x.Id == id)
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

                return _mapper.Map<T>(nodeType);
            }

            if (typeof(InterfaceLibCm).IsAssignableFrom(typeof(T)))
            {
                var interfaceType = await _interfaceRepository.FindBy(x => x.Id == id)
                    .Include(x => x.Attributes)
                    .OrderBy(x => x.Name)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();

                return _mapper.Map<T>(interfaceType);
            }

            if (typeof(TransportLibCm).IsAssignableFrom(typeof(T)))
            {
                var transportType = await _transportRepository.FindBy(x => x.Id == id)
                    .Include(x => x.Attributes)
                    .OrderBy(x => x.Name)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();

                return _mapper.Map<T>(transportType);
            }

            return null;
        }

        public void ClearAllChangeTracker()
        {
            _transportRepository?.Context?.ChangeTracker.Clear();
            _interfaceRepository?.Context?.ChangeTracker.Clear();
            _nodeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}