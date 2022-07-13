using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories.Application
{
    public class ApplicationSettingsRepository : IApplicationSettingsRepository
    {
        public string ApplicationSemanticUrl { get; }
        public string ApplicationUrl { get; }

        public ApplicationSettingsRepository(IOptions<ApplicationSettings> applicationSettings)
        {
            ApplicationSemanticUrl = applicationSettings?.Value?.ApplicationSemanticUrl;
            ApplicationUrl = applicationSettings?.Value?.ApplicationUrl;
        }
    }
}