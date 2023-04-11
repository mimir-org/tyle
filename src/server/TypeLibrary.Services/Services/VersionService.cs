using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class VersionService : IVersionService
{
    private readonly IAspectObjectRepository _aspectObjectRepository;
    private readonly ITerminalRepository _terminalRepository;

    public VersionService(IAspectObjectRepository aspectObjectRepository, ITerminalRepository terminalRepository)
    {
        _aspectObjectRepository = aspectObjectRepository;
        _terminalRepository = terminalRepository;
    }

    /// <summary>
    /// Method will find and return the latest version.
    /// </summary>
    /// <typeparam name="T">AspectObjectLibDm, TerminalLibDm or AttributeLibDm</typeparam>
    /// <param name="obj">AspectObjectLibDm, TerminalLibDm or AttributeLibDm</param>
    /// <returns>Latest version of AspectObjectLibDm, TerminalLibDm or AttributeLibDm</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    public async Task<T> GetLatestVersion<T>(T obj) where T : class
    {
        if (obj?.GetType() is null)
            throw new MimirorgBadRequestException("GetLatestVersion<T> Parameter T can't be null.");

        var existingDmVersions = new List<T>();

        if (obj.GetType() == typeof(AspectObjectLibDm) && (obj as AspectObjectLibDm)?.Version != null)
        {
            (existingDmVersions as List<AspectObjectLibDm>)?.AddRange(_aspectObjectRepository.Get()
                .Where(x => x.FirstVersionId == (obj as AspectObjectLibDm)?.FirstVersionId && x.State != State.Deleted).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
        }

        else if (obj.GetType() == typeof(TerminalLibDm) && (obj as TerminalLibDm)?.Version != null)
        {
            (existingDmVersions as List<TerminalLibDm>)?.AddRange(_terminalRepository.Get()
                .Where(x => x.FirstVersionId == (obj as TerminalLibDm)?.FirstVersionId && x.State != State.Deleted).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
        }

        else
            throw new MimirorgBadRequestException($"GetLatestVersion<T> Parameter T '{obj.GetType()}' not supported.");

        if (existingDmVersions == null || !existingDmVersions.Any())
            throw new MimirorgBadRequestException($"No {obj.GetType()} versions found.");

        return await Task.FromResult(existingDmVersions[^1]);
    }
}