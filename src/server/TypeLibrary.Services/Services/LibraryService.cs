using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleRepository _simpleTypeRepository;
        private readonly IMapper _mapper;
        private readonly ITransportRepository _transportRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IRdsRepository _rdsRepository;

        #region Constructors

        public LibraryService(IAttributeRepository attributeRepository, ISimpleRepository simpleTypeRepository, IMapper mapper, ITransportRepository transportRepository, INodeRepository nodeRepository, IRdsRepository rdsRepository)
        {
            _mapper = mapper;
            _transportRepository = transportRepository;
            _nodeRepository = nodeRepository;
            _rdsRepository = rdsRepository;
            _attributeRepository = attributeRepository;
            _simpleTypeRepository = simpleTypeRepository;
        }

        #endregion

        #region Aspect Nodes

        /// <summary>
        /// Get the aspect node from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the id param is null or whitespace</exception>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public async Task<NodeLibCm> GetNode(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get aspect node. The id is a missing value.");

            var node = await _nodeRepository.FindNode(id).FirstOrDefaultAsync();
            if (node == null)
                throw new MimirorgNotFoundException($"There is no aspect node with id: {id}");

            var nodeLibCm = _mapper.Map<NodeLibCm>(node);
            if (nodeLibCm == null)
                throw new MimirorgMappingException(nameof(NodeLibDm), nameof(NodeLibCm));

            return nodeLibCm;
        }

        /// <summary>
        /// Get all registered aspect nodes
        /// </summary>
        /// <returns>A collection of aspect nodes. If there is none, an empty list is returned.</returns>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public Task<ICollection<NodeLibCm>> GetNodes()
        {
            var nodes = _nodeRepository.GetAllNodes().ToList();
            var nodeLibCms = _mapper.Map<ICollection<NodeLibCm>>(nodes);

            if (nodes.Any() && (nodeLibCms == null || !nodeLibCms.Any()))
                throw new MimirorgMappingException(nameof(ICollection<NodeLibDm>), nameof(ICollection<NodeLibCm>));

            return Task.FromResult(nodeLibCms ?? new List<NodeLibCm>());
        }

        /// <summary>
        /// Create new aspect node from array of aspect node application models
        /// </summary>
        /// <param name="nodes">A collection of nodes that should be created.</param>
        /// <returns>A completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if there is one invalid aspect node</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public async Task CreateNodes(ICollection<NodeLibAm> nodes)
        {
            if (nodes == null || !nodes.Any())
                return;

            var existingNodeTypes = await GetNodes();
            var nodesToCreate = nodes.Where(x => existingNodeTypes.All(y => y.Id != x.Id)).ToList();
            var nodeDmList = _mapper.Map<ICollection<NodeLibDm>>(nodesToCreate);

            if (nodeDmList == null || (!nodeDmList.Any() && nodesToCreate.Any()))
                throw new MimirorgMappingException(nameof(ICollection<NodeLibAm>), nameof(ICollection<NodeLibDm>));

            var allRds = _rdsRepository.GetAll();

            foreach (var nodeLibDm in nodeDmList)
            {
                var validation = nodes.ValidateObject();
                if (!validation.IsValid)
                    throw new MimirorgBadRequestException("Couldn't create node. The aspect node is not valid.", validation);

                nodeLibDm.RdsName = allRds.FirstOrDefault(x => x.Id == nodeLibDm.RdsId)?.Name;

                // TODO: This one will probably not work
                _attributeRepository.Attach(nodeLibDm.Attributes, EntityState.Unchanged);
                await _nodeRepository.CreateAsync(nodeLibDm);
                await _nodeRepository.SaveAsync();
                _attributeRepository.Detach(nodeLibDm.Attributes);
                _nodeRepository.Detach(nodeLibDm);
            }
        }

        /// <summary>
        /// Create a new aspect node
        /// </summary>
        /// <param name="node">The aspect node application model that should be created</param>
        /// <returns>The created aspect node</returns>
        /// <exception cref="MimirorgNullReferenceException">Throws if parameter is null</exception>
        /// <exception cref="MimirorgBadRequestException">Throws if the node is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if the node already exist</exception>
        /// <exception cref="MimirorgMappingException">Throws if the mapped object is null</exception>
        public async Task<NodeLibCm> CreateNode(NodeLibAm node)
        {
            if (node == null)
                throw new MimirorgNullReferenceException("Can't create an aspect node from null object.");

            var validation = node.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create an aspect node. The node is not valid.", validation);

            var exitingNode = await _nodeRepository.GetAsync(node.Id);
            if (exitingNode != null)
                throw new MimirorgDuplicateException($"There is already registered a aspect node with name: {node.Name}");

            var nodeLibDm = _mapper.Map<NodeLibDm>(node);
            if (nodeLibDm == null)
                throw new MimirorgMappingException(nameof(NodeLibAm), nameof(NodeLibDm));

            nodeLibDm.RdsName = _rdsRepository.FindBy(x => x.Id == nodeLibDm.RdsId)?.First()?.Name;

            // TODO: This will probably not work. Some other objects must be detached.
            _attributeRepository.Attach(nodeLibDm.Attributes, EntityState.Unchanged);
            await _nodeRepository.CreateAsync(nodeLibDm);
            await _nodeRepository.SaveAsync();
            _attributeRepository.Detach(nodeLibDm.Attributes);
            _nodeRepository.Detach(nodeLibDm);

            var createdObject = _mapper.Map<NodeLibCm>(nodeLibDm);
            if (createdObject == null)
                throw new MimirorgMappingException(nameof(NodeLibDm), nameof(NodeLibCm));

            return createdObject;
        }

        public Task DeleteNode(string id, string version)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Interfaces

        public Task<InterfaceLibCm> GetInterface(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<InterfaceLibCm>> GetInterfaces()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<InterfaceLibCm>> CreateInterfaces(ICollection<InterfaceLibAm> interfaces)
        {
            throw new System.NotImplementedException();
        }

        public Task<InterfaceLibCm> CreateTransport(InterfaceLibAm interfaceAm)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Transports

        /// <summary>
        /// Get the transport from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the id param is null or whitespace</exception>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public async Task<TransportLibCm> GetTransport(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get transport. The id is missing value.");

            var transport = await _transportRepository.FindTransport(id).FirstOrDefaultAsync();
            if (transport == null)
                throw new MimirorgNotFoundException($"There is no transport with id: {id}");

            var transportLibCm = _mapper.Map<TransportLibCm>(transport);
            if (transportLibCm == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return transportLibCm;
        }

        /// <summary>
        /// Get all registered transports
        /// </summary>
        /// <returns>A collection of transport. If there is none, an empty list is returned.</returns>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public Task<ICollection<TransportLibCm>> GetTransports()
        {
            var transports = _transportRepository.GetAllTransports().ToList();
            var transportLibCms = _mapper.Map<ICollection<TransportLibCm>>(transports);

            if(transports.Any() && (transportLibCms == null || !transportLibCms.Any()))
                throw new MimirorgMappingException("List<TransportLibDm>", "ICollection<TransportLibAm>");

            return Task.FromResult(transportLibCms ?? new List<TransportLibCm>());
        }

        /// <summary>
        /// Create new transports from array of transports
        /// </summary>
        /// <param name="transports">A collection of transports that should be created.</param>
        /// <returns>A collection of created transports</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        public async Task<ICollection<TransportLibCm>> CreateTransports(ICollection<TransportLibAm> transports)
        {
            var validation = transports.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create transports", validation);

            var existingTransportTypes = await GetTransports();
            var transportsToCreate = transports.Where(x => existingTransportTypes.All(y => y.Id != x.Id)).ToList();
            var transportDmList = _mapper.Map<ICollection<TransportLibDm>>(transportsToCreate);

            if (transportDmList == null || (!transportDmList.Any() && transportsToCreate.Any()))
                throw new MimirorgMappingException("ICollection<TransportLibDm>", "ICollection<TransportLibAm>");

            var allRds = _rdsRepository.GetAll();

            foreach (var transportLibDm in transportDmList)
            {
                transportLibDm.RdsName = allRds.FirstOrDefault(x => x.Id == transportLibDm.RdsId)?.Name;
                _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);
                await _transportRepository.CreateAsync(transportLibDm);
                await _transportRepository.SaveAsync();
                _attributeRepository.Detach(transportLibDm.Attributes);
                _transportRepository.Detach(transportLibDm);
            }

            return _mapper.Map<ICollection<TransportLibCm>>(transportDmList);
        }

        /// <summary>
        /// Create a new transport
        /// </summary>
        /// <param name="transport">The transport object that should be created</param>
        /// <returns>The created transport object</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgMappingException"></exception>
        public async Task<TransportLibCm> CreateTransport(TransportLibAm transport)
        {
            var existingTransport = await _transportRepository.GetAsync(transport.Id);
            if(existingTransport != null)
                throw new MimirorgBadRequestException($"There is already registered a transport with name: {transport.Name} with version: {transport.Version}");

            var transportLibDm = _mapper.Map<TransportLibDm>(transport);
            if (transportLibDm == null)
                throw new MimirorgMappingException("TransportLibAm", "TransportLibDm");

            transportLibDm.RdsName = _rdsRepository.FindBy(x => x.Id == transportLibDm.RdsId)?.First()?.Name;

            _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);
            await _transportRepository.CreateAsync(transportLibDm);
            await _transportRepository.SaveAsync();
            _attributeRepository.Detach(transportLibDm.Attributes);
            _transportRepository.Detach(transportLibDm);

            var createdObject = _mapper.Map<TransportLibCm>(transportLibDm);
            if (createdObject == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return createdObject;
        }

        #endregion

        #region Simple

        /// <summary>
        /// Create a new simple type
        /// </summary>
        /// <param name="simpleAm">The simple type to create</param>
        /// <returns>The created simple type</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the simple type application model is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if the simple type application model already is created</exception>
        /// <exception cref="MimirorgMappingException">Throws if the simple type application model mapping returns null</exception>
        public async Task<SimpleLibCm> CreateSimple(SimpleLibAm simpleAm)
        {
            var validation = simpleAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create simple", validation);

            var s = await _simpleTypeRepository.GetAsync(simpleAm.Id);
            if (s != null)
                throw new MimirorgDuplicateException($"There is already a simple with name: {simpleAm.Name}");

            var dmObject = _mapper.Map<SimpleLibDm>(simpleAm);
            if (dmObject == null)
                throw new MimirorgMappingException(nameof(SimpleLibAm), nameof(SimpleLibDm));

            
            _attributeRepository.Attach(dmObject.Attributes, EntityState.Unchanged);
            await _simpleTypeRepository.CreateAsync(dmObject);
            await _simpleTypeRepository.SaveAsync();
            _attributeRepository.Detach(dmObject.Attributes);
            _simpleTypeRepository.Detach(dmObject);

            var cm = _mapper.Map<SimpleLibCm>(dmObject);
            if (cm == null)
                throw new MimirorgMappingException(nameof(SimpleLibDm), nameof(SimpleLibCm));

            return cm;
        }

        /// <summary>
        /// Create simple types from a collection of simple application models
        /// </summary>
        /// <param name="simpleAmList">The collection of simple application models that should be created</param>
        /// <returns>Returns completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if any model is not valid</exception>
        /// <exception cref="MimirorgMappingException">Throws if any mapping result return null reference object</exception>
        public async Task CreateSimple(ICollection<SimpleLibAm> simpleAmList)
        {
            if (simpleAmList == null || !simpleAmList.Any())
                return;

            foreach (var simpleAm in simpleAmList)
            {
                var validation = simpleAm.ValidateObject();
                if (!validation.IsValid)
                    throw new MimirorgBadRequestException("Couldn't create simple", validation);

                var s = await _simpleTypeRepository.GetAsync(simpleAm.Id);
                if (s != null)
                    continue;

                var dmObject = _mapper.Map<SimpleLibDm>(simpleAm);
                if (dmObject == null)
                    throw new MimirorgMappingException(nameof(SimpleLibAm), nameof(SimpleLibDm));

                _attributeRepository.Attach(dmObject.Attributes, EntityState.Unchanged);
                await _simpleTypeRepository.CreateAsync(dmObject);
                await _simpleTypeRepository.SaveAsync();
                _attributeRepository.Detach(dmObject.Attributes);
                _simpleTypeRepository.Detach(dmObject);
            }
        }

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns>A collection of simple types</returns>
        /// <exception cref="MimirorgMappingException">Throws if mapping to client models is null or empty</exception>
        public Task<ICollection<SimpleLibCm>> GetSimples()
        {
            var allSimples = _simpleTypeRepository.GetAllSimples().ToList();
            var data = _mapper.Map<ICollection<SimpleLibCm>>(allSimples);

            if (data == null || allSimples.Any() && !data.Any())
                throw new MimirorgMappingException("There is mismatch between fetch data and mapped data.");

            return Task.FromResult(data);
        }

        #endregion

        #region Common

        /// <summary>
        /// Clear the change trackers
        /// </summary>
        public void ClearAllChangeTracker()
        {
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _simpleTypeRepository?.Context?.ChangeTracker.Clear();
            _transportRepository?.Context?.ChangeTracker.Clear();
            _nodeRepository?.Context?.ChangeTracker.Clear();
        }

        #endregion


        //private void SetLibraryTypeVersion(LibraryTypeLibDm newLibraryTypeDm, LibraryTypeLibDm existingLibraryTypeDm)
        //{
        //    if (string.IsNullOrWhiteSpace(newLibraryTypeDm?.Version) || string.IsNullOrWhiteSpace(existingLibraryTypeDm?.Version))
        //        throw new MimirorgInvalidOperationException("'Null' error when setting version for library typeAm");

        //    //TODO: The rules for when to trigger major/minor version incrementation is not finalized!

        //    //typeDm.Version = existingTypeDm.Version.IncrementMinorVersion();

        //}



    }
}
