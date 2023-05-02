using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class AttributeIriResolver : IValueResolver<AttributeLibAm, AttributeLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public AttributeIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(AttributeLibAm src, AttributeLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/attribute/{dest.Id}";
    }
}