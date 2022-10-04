using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IInterfaceService
    {
        /// <summary>
        /// Get the latest version of an interface based on given id
        /// </summary>
        /// <param name="id">The id of the interface</param>
        /// <returns>The latest version of the interface of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no interface with the given id, and that interface is at the latest version.</exception>
        InterfaceLibCm GetLatestVersion(string id);

        /// <summary>
        /// Get the latest interface versions
        /// </summary>
        /// <returns>A collection of interface</returns>
        IEnumerable<InterfaceLibCm> GetLatestVersions();

        /// <summary>
        /// Create a new interface
        /// </summary>
        /// <param name="interfaceAm">The interface that should be created</param>
        /// <returns>The created interface</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if interface is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if interface already exist</exception>
        /// <remarks>Remember that creating a new interface could be creating a new version of existing interface.
        /// They will have the same first version id, but have different version and id.</remarks>
        Task<InterfaceLibCm> Create(InterfaceLibAm interfaceAm);

        /// <summary>
        /// Update an interface if the data is allowed to be changed.
        /// </summary>
        /// <param name="interfaceAm">The interface to update</param>
        /// <returns>The updated interface</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the interface does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        Task<InterfaceLibCm> Update(InterfaceLibAm interfaceAm);

        /// <summary>
        /// Change interface state
        /// </summary>
        /// <param name="id">The interface id that should change the state</param>
        /// <param name="state">The new interface state</param>
        /// <returns>Interface with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the interface does not exist on latest version</exception>
        Task<InterfaceLibCm> ChangeState(string id, State state);

        /// <summary>
        /// Get interface existing company id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>Company id for the interface</returns>
        Task<int> GetCompanyId(string id);
    }
}