using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ILibraryService
    {
        #region Apspect Nodes

        /// <summary>
        /// Get the aspect node from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Node</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the id param is null or whitespace</exception>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<NodeLibCm> GetNode(string id);

        /// <summary>
        /// Get all registered aspect nodes
        /// </summary>
        /// <returns>A collection of aspect nodes. If there is none, an empty list is returned.</returns>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<ICollection<NodeLibCm>> GetNodes();

        /// <summary>
        /// Create new aspect node from array of aspect node application models
        /// </summary>
        /// <param name="nodes">A collection of nodes that should be created.</param>
        /// <returns>A completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if there is one invalid aspect node</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task CreateNodes(ICollection<NodeLibAm> nodes);

        /// <summary>
        /// Create a new aspect node
        /// </summary>
        /// <param name="node">The aspect node application model that should be created</param>
        /// <returns>The created aspect node</returns>
        /// <exception cref="MimirorgNullReferenceException">Throws if parameter is null</exception>
        /// <exception cref="MimirorgBadRequestException">Throws if the node is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if the node already exist</exception>
        /// <exception cref="MimirorgMappingException">Throws if the mapped object is null</exception>
        Task<NodeLibCm> CreateNode(NodeLibAm node);

        Task DeleteNode(string id, string version);

        #endregion

        #region Interfaces

        /// <summary>
        /// Get the interface from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the id param is null or whitespace</exception>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<InterfaceLibCm> GetInterface(string id);

        /// <summary>
        /// Get all registered interfaces
        /// </summary>
        /// <returns>A collection of interfaces. If there is none, an empty list is returned.</returns>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<ICollection<InterfaceLibCm>> GetInterfaces();

        /// <summary>
        /// Create new interfaces from array of interface application models
        /// </summary>
        /// <param name="interfaces">A collection of transports that should be created.</param>
        /// <returns>A collection of created transports</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<ICollection<InterfaceLibCm>> CreateInterfaces(ICollection<InterfaceLibAm> interfaces);

        /// <summary>
        /// Create a new interface
        /// </summary>
        /// <param name="interfaceAm">The interface object that should be created</param>
        /// <returns>The created transport object</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgMappingException"></exception>
        Task<InterfaceLibCm> CreateTransport(InterfaceLibAm interfaceAm);

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
        Task<TransportLibCm> GetTransport(string id);

        /// <summary>
        /// Get all registered transports
        /// </summary>
        /// <returns>A collection of transport. If there is none, an empty list is returned.</returns>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<ICollection<TransportLibCm>> GetTransports();

        /// <summary>
        /// Create new transports from array of transport application models
        /// </summary>
        /// <param name="transports">A collection of transports that should be created.</param>
        /// <returns>A collection of created transports</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if there is no matching object on given id</exception>
        /// <exception cref="MimirorgMappingException">Throws if there is no matching object on given id</exception>
        Task<ICollection<TransportLibCm>> CreateTransports(ICollection<TransportLibAm> transports);

        /// <summary>
        /// Create a new transport
        /// </summary>
        /// <param name="transport">The transport object that should be created</param>
        /// <returns>The created transport object</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgMappingException"></exception>
        Task<TransportLibCm> CreateTransport(TransportLibAm transport);

        #endregion

        #region Simples

        /// <summary>
        /// Create a new simple type
        /// </summary>
        /// <param name="simpleAm">The simple type to create</param>
        /// <returns>The created simple type</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the simple type application model is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if the simple type application model already is created</exception>
        /// <exception cref="MimirorgMappingException">Throws if the simple type application model mapping returns null</exception>
        Task<SimpleLibCm> CreateSimple(SimpleLibAm simpleAm);

        /// <summary>
        /// Create simple types from a collection of simple application models
        /// </summary>
        /// <param name="simpleAmList">The collection of simple application models that should be created</param>
        /// <returns>Returns completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if any model is not valid</exception>
        /// <exception cref="MimirorgMappingException">Throws if any mapping result return null reference object</exception>
        Task CreateSimple(ICollection<SimpleLibAm> simpleAmList);

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns>A collection of simple types</returns>
        /// <exception cref="MimirorgMappingException">Throws if mapping to client models is null or empty</exception>
        Task<ICollection<SimpleLibCm>> GetSimples();

        #endregion

        #region Common

        /// <summary>
        /// Clear the change trackers
        /// </summary>
        void ClearAllChangeTracker();

        #endregion
    }
}