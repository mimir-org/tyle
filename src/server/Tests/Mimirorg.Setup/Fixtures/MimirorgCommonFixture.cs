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
        public Mock<IAttributeRepository> AttributeRepository = new();
        public Mock<IAttributeQualifierRepository> AttributeQualifierRepository = new();
        public Mock<IAttributeSourceRepository> AttributeSourceRepository = new();
        public Mock<IAttributeFormatRepository> AttributeFormatRepository = new();
        public Mock<IAttributeConditionRepository> AttributeConditionRepository = new();
        public Mock<IAttributePredefinedRepository> AttributePredefinedRepository = new();

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