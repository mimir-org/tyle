using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class AspectObjectIriResolver : IValueResolver<AspectObjectLibAm, AspectObjectLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public AspectObjectIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(AspectObjectLibAm src, AspectObjectLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/aspectobject/{dest.Id}";
    }
}