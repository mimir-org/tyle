using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Abstractions;
using Tyle.Application.Blocks;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Converters.Iris;
using Tyle.Core.Blocks;
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
        await SyncSymbols();
    }

    private async Task SyncPurposes()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        IDownstreamApi? downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        IPurposeRepository? purposeRepository = scope.ServiceProvider.GetService<IPurposeRepository>();

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

        var purposesDb = await purposeRepository.GetAll();

        var newPurposes = purposes.Where(purpose => purposesDb.All(purposeDb => purposeDb.Iri != new Uri(purpose.Identity)));

        foreach (var purpose in newPurposes)
        {
            var purposeRequest = new RdlPurposeRequest
            {
                Name = purpose.Name,
                Description = purpose.Description.Length > 0 ? purpose.Description : null,
                Iri = purpose.Identity
            };

            await purposeRepository.Create(purposeRequest);
        }
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