using AutoMapper;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Core.Profiles.Resolvers;

public class BlockIriResolver : IValueResolver<BlockLibAm, BlockLibDm, string>
{
    private readonly IApplicationSettingsRepository _settings;

    public BlockIriResolver(IApplicationSettingsRepository settings)
    {
        _settings = settings;
    }

    public string Resolve(BlockLibAm src, BlockLibDm dest, string iri, ResolutionContext context)
    {
        return $"{_settings.ApplicationSemanticUrl}/block/{dest.Id}";
    }
}