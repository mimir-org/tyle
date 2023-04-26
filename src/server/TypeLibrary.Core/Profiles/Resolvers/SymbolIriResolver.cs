using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class SymbolIriResolver : IValueResolver<SymbolLibAm, SymbolLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public SymbolIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(SymbolLibAm src, SymbolLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/symbol/{dest.Id}";
    }
}