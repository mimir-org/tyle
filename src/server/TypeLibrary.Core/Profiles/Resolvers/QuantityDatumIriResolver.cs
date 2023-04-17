using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class QuantityDatumIriResolver : IValueResolver<QuantityDatumLibAm, QuantityDatumLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public QuantityDatumIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(QuantityDatumLibAm src, QuantityDatumLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/quantitydatum/{dest.Id}";
    }
}