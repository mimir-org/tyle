using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface ITransportService
    {
        /// <summary>
        /// Get the latest version of a transport based on given id
        /// </summary>
        /// <param name="id">The id of the transport</param>
        /// <returns>The latest version of the transport of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no transport with the given id, and that transport is at the latest version.</exception>
        TransportLibCm GetLatestVersion(string id);

        /// <summary>
        /// Get the latest transport versions
        /// </summary>
        /// <returns>A collection of transport</returns>
        IEnumerable<TransportLibCm> GetLatestVersions();

        /// <summary>
        /// Create a new transport
        /// </summary>
        /// <param name="transportAm">The transport that should be created</param>
        /// <returns>The created transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if transport is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if transport already exist</exception>
        /// <remarks>Remember that creating a new transport could be creating a new version of existing transport.
        /// They will have the same first version id, but have different version and id.</remarks>
        Task<TransportLibCm> Create(TransportLibAm transportAm);

        /// <summary>
        /// Update an transport if the data is allowed to be changed.
        /// </summary>
        /// <param name="transportAm">The transport to update</param>
        /// <returns>The updated transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the transport does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        Task<TransportLibCm> Update(TransportLibAm transportAm);

        /// <summary>
        /// Change transport state
        /// </summary>
        /// <param name="id">The transport id that should change the state</param>
        /// <param name="state">The new transport state</param>
        /// <returns>Transport with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the transport does not exist on latest version</exception>
        Task<TransportLibCm> ChangeState(string id, State state);

        /// <summary>
        /// Get transport existing company id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>Company id for the transport</returns>
        Task<int> GetCompanyId(string id);
    }
}