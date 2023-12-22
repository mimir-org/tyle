using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Abstractions;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Blocks;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Converters.Iris;
using Tyle.Core.Blocks;
using Tyle.Core.Common;
using Tyle.External.Model;
using VDS.RDF.Parsing;
using VDS.RDF;

namespace Tyle.External;

public class CommonLibSyncingService : IHostedService, IDisposable
{
    private bool _disposedValue;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer = null;

    public CommonLibSyncingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var nextSyncTime = new DateTime(now.Year, now.Month, now.Day, 2, 0, 0).AddDays(1);

        _timer = new Timer(Sync, null, TimeSpan.FromSeconds((nextSyncTime - now).TotalSeconds), TimeSpan.FromDays(1));

        Sync(null);

        return Task.CompletedTask;
    }

    private async void Sync(object? state)
    {
        await SyncPurposes();
        await SyncMedia();
        await SyncClassifiers();
        await SyncPredicates();
        await SyncUnits();
        await SyncSymbols();
    }

    private async Task SyncPurposes()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var purposeRepository = scope.ServiceProvider.GetService<IPurposeRepository>();

        if (downstreamApi == null || purposeRepository == null)
        {
            return;
        }

        var purposes = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFPurpose";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (purposes == null)
        {
            return;
        }

        var purposesDb = (await purposeRepository.GetAll()).ToList();

        var purposesToCreate = new List<RdlPurposeRequest>();

        foreach (var purpose in purposes)
        {
            var iriAttribute = purpose.Attributes.FirstOrDefault(x => x.DefinitionName == "Identifier");
            if (iriAttribute == null || !Uri.TryCreate(iriAttribute.DisplayValue, UriKind.Absolute, out var iri) || purposesDb.Any(savedPurpose => savedPurpose.Iri.Equals(iri)))
            {
                continue;
            }

            var purposeRequest = new RdlPurposeRequest
            {
                Name = purpose.Name,
                Description = purpose.Description.Length > 0 ? purpose.Description : null,
                Iri = iriAttribute.DisplayValue
            };

            if (purposesToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(purposeRequest.Iri)))) continue;

            purposesToCreate.Add(purposeRequest);
        }

        await purposeRepository.Create(purposesToCreate, ReferenceSource.CommonLibrary);
    }

    private async Task SyncMedia()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var mediumRepository = scope.ServiceProvider.GetService<IMediumRepository>();

        if (downstreamApi == null || mediumRepository == null)
        {
            return;
        }

        var media = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFMedium";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (media == null)
        {
            return;
        }

        var mediaDb = (await mediumRepository.GetAll()).ToList();

        var mediaToCreate = new List<RdlMediumRequest>();

        foreach (var medium in media)
        {
            var iriAttribute = medium.Attributes.FirstOrDefault(x => x.DefinitionName == "Identifier");
            if (iriAttribute == null || !Uri.TryCreate(iriAttribute.DisplayValue, UriKind.Absolute, out var iri) || mediaDb.Any(savedMedia => savedMedia.Iri.Equals(iri)))
            {
                continue;
            }

            var mediumRequest = new RdlMediumRequest
            {
                Name = medium.Name,
                Description = medium.Description.Length > 0 ? medium.Description : null,
                Iri = iriAttribute.DisplayValue
            };

            if (mediaToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(mediumRequest.Iri)))) continue;

            mediaToCreate.Add(mediumRequest);
        }

        await mediumRepository.Create(mediaToCreate, ReferenceSource.CommonLibrary);
    }

    private async Task SyncClassifiers()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var classifierRepository = scope.ServiceProvider.GetService<IClassifierRepository>();

        if (downstreamApi == null || classifierRepository == null)
        {
            return;
        }

        var classifiers = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFClassifier";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (classifiers == null)
        {
            return;
        }

        var classifiersDb = (await classifierRepository.GetAll()).ToList();

        var classifiersToCreate = new List<RdlClassifierRequest>();

        foreach (var classifier in classifiers)
        {
            var iriAttribute = classifier.Attributes.FirstOrDefault(x => x.DefinitionName == "Identifier");
            if (iriAttribute == null || !Uri.TryCreate(iriAttribute.DisplayValue, UriKind.Absolute, out var iri) || classifiersDb.Any(savedClassifier => savedClassifier.Iri.Equals(iri)))
            {
                continue;
            }

            var classifierRequest = new RdlClassifierRequest
            {
                Name = classifier.Name,
                Description = classifier.Description.Length > 0 ? classifier.Description : null,
                Iri = iriAttribute.DisplayValue
            };

            if (classifiersToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(classifierRequest.Iri)))) continue;

            classifiersToCreate.Add(classifierRequest);
        }

        await classifierRepository.Create(classifiersToCreate, ReferenceSource.CommonLibrary);
    }

    private async Task SyncPredicates()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var predicateRepository = scope.ServiceProvider.GetService<IPredicateRepository>();

        if (downstreamApi == null || predicateRepository == null)
        {
            return;
        }

        var predicates = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFPredicate";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (predicates == null)
        {
            return;
        }

        var predicatesDb = (await predicateRepository.GetAll()).ToList();

        var predicatesToCreate = new List<RdlPredicateRequest>();

        foreach (var predicate in predicates)
        {
            var iriAttribute = predicate.Attributes.FirstOrDefault(x => x.DefinitionName == "Identifier");
            if (iriAttribute == null || !Uri.TryCreate(iriAttribute.DisplayValue, UriKind.Absolute, out var iri) || predicatesDb.Any(savedPredicate => savedPredicate.Iri.Equals(iri)))
            {
                continue;
            }

            var predicateRequest = new RdlPredicateRequest
            {
                Name = predicate.Name,
                Description = predicate.Description.Length > 0 ? predicate.Description : null,
                Iri = iriAttribute.DisplayValue
            };

            if (predicatesToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(predicateRequest.Iri)))) continue;

            predicatesToCreate.Add(predicateRequest);
        }

        await predicateRepository.Create(predicatesToCreate, ReferenceSource.CommonLibrary);
    }

    private async Task SyncUnits()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var unitRepository = scope.ServiceProvider.GetService<IUnitRepository>();

        if (downstreamApi == null || unitRepository == null)
        {
            return;
        }

        var units = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFUnitOfMeasure";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (units == null)
        {
            return;
        }

        var unitsDb = (await unitRepository.GetAll()).ToList();

        var unitsToCreate = new List<RdlUnitRequest>();

        foreach (var unit in units)
        {
            var iriAttribute = unit.Attributes.FirstOrDefault(x => x.DefinitionName == "Identifier");
            if (iriAttribute == null || !Uri.TryCreate(iriAttribute.DisplayValue, UriKind.Absolute, out var iri) || unitsDb.Any(savedUnit => savedUnit.Iri.Equals(iri)))
            {
                continue;
            }

            var unitRequest = new RdlUnitRequest
            {
                Name = unit.Name,
                Description = unit.Description.Length > 0 ? unit.Description : null,
                Iri = iriAttribute.DisplayValue
            };

            if (unitsToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(unitRequest.Iri)))) continue;

            unitsToCreate.Add(unitRequest);
        }

        await unitRepository.Create(unitsToCreate, ReferenceSource.CommonLibrary);
    }

    private async Task SyncSymbols()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        var symbolRepository = scope.ServiceProvider.GetService<ISymbolRepository>();

        if (downstreamApi == null || symbolRepository == null)
        {
            return;
        }

        var response = await downstreamApi.CallApiForAppAsync("CommonLib", options =>
        {
            options.HttpMethod = "POST";
            options.RelativePath = "api/symbol/ReadEngineeringSymbol?allVersions=false";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";

            options.CustomizeHttpRequestMessage = message =>
            {
                message.Content = new StringContent("", System.Text.Encoding.UTF8, "application/json-patch+json");
            };
        });

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var ts = new TripleStore();
        var parser = new JsonLdParser();

        var responseContent = await response.Content.ReadAsStringAsync();

        using TextReader reader = new StringReader(responseContent);

        parser.Load(ts, reader);

        var symbolsDb = (await symbolRepository.GetAll()).ToList();

        var symbolsToCreate = new List<EngineeringSymbol>();

        foreach (var graph in ts.Graphs)
        {
            var typeTriples = graph.GetTriplesWithPredicateObject(graph.GetUriNode(Rdf.Type), graph.GetUriNode(Sym.Symbol)).ToList();

            if (typeTriples.Count != 1)
            {
                continue;
            }

            var symbolNode = typeTriples.First().Subject;

            if (symbolsDb.Any(savedSymbol => savedSymbol.Iri.Equals(new Uri(symbolNode.ToString()))))
            {
                continue;
            }

            var symbolLabelTriple = graph.GetTriplesWithSubjectPredicate(symbolNode, graph.GetUriNode(Rdfs.Label)).ToList();
            var hasSerializationTriple = graph.GetTriplesWithPredicate(Sym.HasSerialization).ToList();

            if (symbolLabelTriple.Count != 1 || hasSerializationTriple.Count != 1)
            {
                continue;
            }

            var symbol = new EngineeringSymbol
            {
                Label = ((LiteralNode) symbolLabelTriple.First().Object).Value,
                Iri = new Uri(symbolNode.ToString()),
                Path = ((LiteralNode) hasSerializationTriple.First().Object).Value
            };

            var descriptionTriple = graph.GetTriplesWithPredicate(DcTerms.Description);
            var description = ((LiteralNode) descriptionTriple.First().Object).Value;
            if (description.Length > 0)
            {
                symbol.Description = description;
            }

            var heightTriple = graph.GetTriplesWithPredicate(Sym.Height);
            decimal.TryParse(((LiteralNode) heightTriple.First().Object).Value, out var height);
            symbol.Height = height;

            var widthTriple = graph.GetTriplesWithPredicate(Sym.Width);
            decimal.TryParse(((LiteralNode) widthTriple.First().Object).Value, out var width);
            symbol.Width = width;

            var hasConnectionPointTriples = graph.GetTriplesWithPredicate(Sym.HasConnectionPoint);
            foreach (var triple in hasConnectionPointTriples)
            {
                var connectionPointIdentifierTriple = graph.GetTriplesWithSubjectPredicate(triple.Object, graph.GetUriNode(DcTerms.Identifier)).ToList();
                var connectionPointPositionXTriple = graph.GetTriplesWithSubjectPredicate(triple.Object, graph.GetUriNode(Sym.PositionX)).ToList();
                var connectionPointPositionYTriple = graph.GetTriplesWithSubjectPredicate(triple.Object, graph.GetUriNode(Sym.PositionY)).ToList();

                if (connectionPointIdentifierTriple.Count != 1 || connectionPointPositionXTriple.Count != 1 || connectionPointPositionYTriple.Count != 1)
                {
                    continue;
                }

                var parsedPositionX = decimal.TryParse(((LiteralNode) connectionPointPositionXTriple.First().Object).Value, out var connectionPointPositionX);
                var parsedPositionY = decimal.TryParse(((LiteralNode) connectionPointPositionYTriple.First().Object).Value, out var connectionPointPositionY);

                if (!parsedPositionX || !parsedPositionY)
                {
                    continue;
                }

                var connectionPoint = new ConnectionPoint
                {
                    Identifier = ((LiteralNode) connectionPointIdentifierTriple.First().Object).Value,
                    PositionX = connectionPointPositionX,
                    PositionY = connectionPointPositionY
                };

                symbol.ConnectionPoints.Add(connectionPoint);
            }

            symbolsToCreate.Add(symbol);
        }

        await symbolRepository.Create(symbolsToCreate);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            _timer?.Dispose();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}