using Moq;
using Tyle.Application.Attributes;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Test.Setup.Fixtures;

public class RequestTestFixture : IDisposable
{
    public Mock<IServiceProvider> ServiceProvider { get; set; }

    public RequestTestFixture()
    {
        var attributeRepository = new Mock<IAttributeRepository>();
        attributeRepository.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new AttributeType
        {
            Id = id,
            Name = "",
            Version = "",
            CreatedBy = ""
        });

        var terminalRepository = new Mock<ITerminalRepository>();
        terminalRepository.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new TerminalType
        {
            Id = id,
            Name = "",
            Version = "",
            CreatedBy = ""
        });

        var classifierRepository = new Mock<IClassifierRepository>();
        classifierRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => new RdlClassifier
        {
            Id = id,
            Name = "",
            Iri = new Uri("http://example.com/classifier")
        });

        var mediumRepository = new Mock<IMediumRepository>();
        mediumRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => new RdlMedium
        {
            Id = id,
            Name = "",
            Iri = new Uri("http://example.com/medium")
        });

        var predicateRepository = new Mock<IPredicateRepository>();
        predicateRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => new RdlPredicate
        {
            Id = id,
            Name = "",
            Iri = new Uri("http://example.com/predicate")
        });

        var purposeRepository = new Mock<IPurposeRepository>();
        purposeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => new RdlPurpose
        {
            Id = id,
            Name = "",
            Iri = new Uri("http://example.com/purpose")
        });

        var unitRepository = new Mock<IUnitRepository>();
        unitRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => new RdlUnit
        {
            Id = id,
            Name = "",
            Iri = new Uri("http://example.com/unit")
        });

        ServiceProvider = new Mock<IServiceProvider>();
        ServiceProvider.Setup(x => x.GetService(typeof(IAttributeRepository))).Returns(attributeRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(ITerminalRepository))).Returns(terminalRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(IClassifierRepository))).Returns(classifierRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(IMediumRepository))).Returns(mediumRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(IPredicateRepository))).Returns(predicateRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(IPurposeRepository))).Returns(purposeRepository.Object);
        ServiceProvider.Setup(x => x.GetService(typeof(IUnitRepository))).Returns(unitRepository.Object);
    }

    public void Dispose()
    {
    }
}