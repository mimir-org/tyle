using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class UnitIriResolver : IValueResolver<UnitLibAm, UnitLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public UnitIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(UnitLibAm src, UnitLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/unit/{dest.Id}";
    }
}