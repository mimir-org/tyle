using System;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;
using Exceptions;

namespace TypeLibrary.Services.Contracts;

public interface ITerminalService
{
    /// <summary>
    /// Get all terminals
    /// </summary>
    /// <returns>A collection of terminals</returns>
    IEnumerable<TerminalTypeView> Get();

    /// <summary>
    /// Get a terminal based on given id
    /// </summary>
    /// <param name="id">The id of the terminal</param>
    /// <returns>The terminal of given id</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException">Throws if there is no terminal with the given id.</exception>
    TerminalTypeView Get(Guid id);

    /// <summary>
    /// Create a new terminal
    /// </summary>
    /// <param name="request">The terminal that should be created</param>
    /// <returns></returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException">Throws if terminal is not valid</exception>
    Task<TerminalTypeView> Create(TerminalTypeRequest request);

    /// <summary>
    /// Update an existing terminal
    /// </summary>
    /// <param name="id">The id of the terminal that should be updated</param>
    /// <param name="terminalAm">The new terminal values</param>
    /// <returns>The updated terminal</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException">Throws if there is no terminal with the given id.</exception>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException">Throws if the new terminal values are not valid.</exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException">Throws if the terminal is not a draft or approved.</exception>
    Task<TerminalTypeView> Update(Guid id, TerminalTypeRequest terminalAm);

    /// <summary>
    ///  Delete a terminal, it can't be approved
    /// </summary>
    /// <param name="id">The id of the terminal to delete</param>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException">Throws if the terminal with the given id is not found.</exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException">Throws if the terminal in question can't be deleted.</exception>
    Task Delete(Guid id);

    /*/// <summary>
    /// Change terminal state
    /// </summary>
    /// <param name="id">The id of the terminal that should change state</param>
    /// <param name="state">The new terminal state</param>
    /// <param name="sendStateEmail"></param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the terminal does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the terminal is already
    /// approved or contains references to deleted or unapproved attributes.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail);*/
}