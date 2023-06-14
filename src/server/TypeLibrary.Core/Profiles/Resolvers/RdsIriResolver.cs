using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class RdsIriResolver : IValueResolver<RdsLibAm, RdsLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public RdsIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(RdsLibAm src, RdsLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/rds/{dest.Id}";
    }
}