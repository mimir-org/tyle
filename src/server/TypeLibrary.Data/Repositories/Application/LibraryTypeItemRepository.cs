using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Repositories.Application
{
    public class LibraryTypeItemRepository : ILibraryTypeItemRepository
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IInterfaceRepository _interfaceRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;

        public LibraryTypeItemRepository(IMapper mapper, ITransportRepository transportRepository,
            IInterfaceRepository interfaceRepository, INodeRepository nodeRepository)
        {
            _mapper = mapper;
            _transportRepository = transportRepository;
            _interfaceRepository = interfaceRepository;
            _nodeRepository = nodeRepository;
        }

        public Task<IEnumerable<NodeLibCm>> GetNodes(string searchString = null)
        {
            var nodeTypes = _nodeRepository.Get();

            if (!string.IsNullOrWhiteSpace(searchString))
                nodeTypes = nodeTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            var mappedItems = nodeTypes.Select(nodeType => _mapper.Map<NodeLibCm>(nodeType));
            return Task.FromResult(mappedItems);
        }

        public Task<IEnumerable<InterfaceLibCm>> GetInterfaces(string searchString = null)
        {
            var interfaceTypes = _interfaceRepository.Get();

            if (!string.IsNullOrWhiteSpace(searchString))
                interfaceTypes = interfaceTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            var cms = interfaceTypes.Select(interfaceType => _mapper.Map<InterfaceLibCm>(interfaceType));
            return Task.FromResult(cms);
        }

        public Task<IEnumerable<TransportLibCm>> GetTransports(string searchString = null)
        {
            var transportTypes = _transportRepository.Get();

            if (!string.IsNullOrWhiteSpace(searchString))
                transportTypes = transportTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            var cms = transportTypes.Select(transportType => _mapper.Map<TransportLibCm>(transportType));
            return Task.FromResult(cms);
        }

        public async Task<T> GetLibraryItem<T>(string id) where T : class, new()
        {
            if (typeof(NodeLibCm).IsAssignableFrom(typeof(T)))
            {
                var nodeType = await _nodeRepository.Get(id);
                return _mapper.Map<T>(nodeType);
            }

            if (typeof(InterfaceLibCm).IsAssignableFrom(typeof(T)))
            {
                var interfaceType = await _interfaceRepository.Get(id);
                return _mapper.Map<T>(interfaceType);
            }

            if (typeof(TransportLibCm).IsAssignableFrom(typeof(T)))
            {
                var transportType = await _transportRepository.Get(id);
                return _mapper.Map<T>(transportType);
            }

            return null;
        }

        public void ClearAllChangeTracker()
        {
            _transportRepository.ClearAllChangeTrackers();
            _interfaceRepository.ClearAllChangeTrackers();
            _nodeRepository.ClearAllChangeTrackers();
        }
    }
}