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
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;

        public LibraryTypeItemRepository(IMapper mapper, INodeRepository nodeRepository)
        {
            _mapper = mapper;
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

        public async Task<T> GetLibraryItem<T>(int id) where T : class, new()
        {
            if (typeof(NodeLibCm).IsAssignableFrom(typeof(T)))
            {
                var nodeType = await _nodeRepository.Get(id);
                return _mapper.Map<T>(nodeType);
            }

            return null;
        }

        public void ClearAllChangeTracker()
        {
            _nodeRepository.ClearAllChangeTrackers();
        }
    }
}