using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Repositories
{
    public class LibraryTypeItemRepository : ILibraryTypeItemRepository
    {
        private readonly ITransportRepository _transportTypeRepository;
        private readonly IInterfaceRepository _interfaceTypeRepository;
        private readonly INodeRepository _nodeTypeRepository;
        private readonly IMapper _mapper;

        public LibraryTypeItemRepository(IMapper mapper, ITransportRepository transportTypeRepository,
            IInterfaceRepository interfaceTypeRepository, INodeRepository nodeTypeRepository)
        {
            _mapper = mapper;
            _transportTypeRepository = transportTypeRepository;
            _interfaceTypeRepository = interfaceTypeRepository;
            _nodeTypeRepository = nodeTypeRepository;
        }

        public async Task<IEnumerable<NodeLibCm>> GetNodes(string searchString = null)
        {
            var nodeTypes = await _nodeTypeRepository.GetAll()
                .Include(x => x.AttributeList)
                    .ThenInclude(x => x.Units)
                .Include(x => x.TerminalTypes)
                    .ThenInclude(x => x.TerminalDm)
                    .ThenInclude(x => x.Attributes)
                    .ThenInclude(x => x.Units)
                .Include(x => x.RdsDm)
                    .ThenInclude(x => x.RdsCategoryDm)
                .Include(x => x.SimpleTypes)
                    .ThenInclude(x => x.AttributeList)
                    .ThenInclude(x => x.Units)
                .Include(x => x.PurposeDm)
                .AsSplitQuery()
                .ToArrayAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
                nodeTypes = nodeTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            return nodeTypes.Select(nodeType => _mapper.Map<NodeLibCm>(nodeType)).ToList();
        }

        public async Task<IEnumerable<InterfaceLibCm>> GetInterfaces(string searchString = null)
        {

            var interfaceTypes = await _interfaceTypeRepository.GetAll()
                .Include(x => x.AttributeList)
                .Include(x => x.RdsDm)
                    .ThenInclude(x => x.RdsCategoryDm)
                .Include(x => x.PurposeDm)
                .OrderBy(x => x.Name)
                .AsSplitQuery()
                .ToArrayAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
                interfaceTypes = interfaceTypes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();

            return interfaceTypes.Select(interfaceType => _mapper.Map<InterfaceLibCm>(interfaceType)).ToList();
        }

        public async Task<IEnumerable<TransportLibCm>> GetTransports(string searchString = null)
        {
            var transportTypes = await _transportTypeRepository.GetAll()
                .Include(x => x.AttributeList)
                .Include(x => x.RdsDm)
                    .ThenInclude(x => x.RdsCategoryDm)
                .Include(x => x.PurposeDm)
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
                var nodeType = await _nodeTypeRepository.FindBy(x => x.Id == id)
                    .Include(x => x.AttributeList)
                    .Include("AttributeList.Units")
                    .Include(x => x.TerminalTypes)
                    .Include("TerminalTypes.TerminalDm")
                    .Include("TerminalTypes.TerminalDm.TerminalCategory")
                    .Include("TerminalTypes.TerminalDm.AttributeList")
                    .Include("TerminalTypes.TerminalDm.AttributeList.Units")
                    .Include(x => x.RdsDm)
                    .Include("RdsDm.RdsCategoryDm")
                    .Include(x => x.SimpleTypes)
                    .Include("SimpleTypes.AttributeList")
                    .Include("SimpleTypes.AttributeList.Units")
                    .Include(x => x.PurposeDm)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();

                return _mapper.Map<T>(nodeType);
            }

            if (typeof(InterfaceLibCm).IsAssignableFrom(typeof(T)))
            {
                var interfaceType = await _interfaceTypeRepository.FindBy(x => x.Id == id)
                    .Include(x => x.AttributeList)
                    .Include(x => x.RdsDm)
                    .Include("RdsDm.RdsCategoryDm")
                    .Include(x => x.PurposeDm)
                    .OrderBy(x => x.Name)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();

                return _mapper.Map<T>(interfaceType);
            }

            if (typeof(TransportLibCm).IsAssignableFrom(typeof(T)))
            {
                var transportType = await _transportTypeRepository.FindBy(x => x.Id == id)
                    .Include(x => x.AttributeList)
                    .Include(x => x.RdsDm)
                    .Include("RdsDm.RdsCategoryDm")
                    .Include(x => x.PurposeDm)
                    .OrderBy(x => x.Name)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();

                return _mapper.Map<T>(transportType);
            }

            return null;
        }

        public void ClearAllChangeTracker()
        {
            _transportTypeRepository?.Context?.ChangeTracker.Clear();
            _interfaceTypeRepository?.Context?.ChangeTracker.Clear();
            _nodeTypeRepository?.Context?.ChangeTracker.Clear();
        }
    }
}