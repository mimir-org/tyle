using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class TerminalIriResolver : IValueResolver<TerminalLibAm, TerminalLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public TerminalIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(TerminalLibAm src, TerminalLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/terminal/{dest.Id}";
    }
}