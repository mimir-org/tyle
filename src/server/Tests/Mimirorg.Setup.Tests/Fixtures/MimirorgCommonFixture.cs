using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Models;
using Moq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace Mimirorg.Setup.Tests.Fixtures
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