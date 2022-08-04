using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Models;
using Moq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace Mimirorg.Setup.Fixtures
{
    public class MimirorgCommonFixture : IDisposable
    {
        // Common
        public ApplicationSettings ApplicationSettings = new();
        public Mock<IMapper> Mapper = new();

        // Repositories
        public Mock<INodeRepository> NodeRepository = new();

        // Services
        public Mock<IVersionService> VersionService = new();
        public Mock<ITimedHookService> TimedHookService = new();

        public MimirorgCommonFixture()
        {
            ApplicationSettings.ApplicationSemanticUrl = @"http://localhost:5001/v1/ont";
            ApplicationSettings.ApplicationUrl = @"http://localhost:5001";

        }

        public void Dispose()
        {

        }
    }
}


//var mappingConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new SourceMappingProfile());
//});
//IMapper mapper = mappingConfig.CreateMapper();
//_mapper = mapper;


//var cfg = new MapperConfigurationExpression();
//cfg.AddProfile(new AttributeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IUnitFactory>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new SymbolProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<IOptions<ApplicationSettings>>()));
//cfg.AddProfile(new NodeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new InterfaceProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new TransportProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new RdsProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new TerminalProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IAttributeFactory>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new AttributeConditionProfile());
//cfg.AddProfile(new AttributeFormatProfile());
//cfg.AddProfile(new AttributeQualifierProfile());
//cfg.AddProfile(new AttributeSourceProfile());
//cfg.AddProfile(new AttributeAspectProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new AttributePredefinedProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new PurposeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new UnitProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new SimpleProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
//cfg.AddProfile(new SelectedAttributePredefinedProfile(provider.GetService<IApplicationSettingsRepository>()));
//cfg.AddProfile(new NodeTerminalProfile());