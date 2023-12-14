using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Abstractions;
using Tyle.Application.Blocks;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Blocks;
using Tyle.External.Model;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
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

        IDownstreamApi? downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        ISymbolRepository? symbolRepository = scope.ServiceProvider.GetService<ISymbolRepository>();

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

        var symbolsDb = await symbolRepository.GetAll();

        var ts = new TripleStore();
        var parser = new JsonLdParser();

        var responseContent = await response.Content.ReadAsStringAsync();

        var symbols = new List<SymbolFromCL>();

        using (TextReader reader = new StringReader(responseContent))
        {
            parser.Load(ts, reader);

            var listSparQlQueryResults = new List<SparqlResultSet>();

            foreach (var graph in ts.Graphs)
            {
                var queryResult = (SparqlResultSet) graph.ExecuteQuery(@"
                SELECT ?subject ?predicate ?object
                WHERE { 
                        ?subject ?predicate ?object .
                }");
                listSparQlQueryResults.Add(queryResult);
            }

            foreach (var items in listSparQlQueryResults)
            {
                var symbol = new SymbolFromCL();
                foreach (var queryResultItem in items)
                {
                    var key = ((UriNode) queryResultItem[1]).ToString();
                    var iriValue = queryResultItem[0].ToString();
                    if (symbol.Iri == null)
                    {
                        if (iriValue.StartsWith("https://rdf.equinor.com/engineering-symbols/") || iriValue.StartsWith("http://example.com/"))
                        {
                            symbol.Iri = new Uri(iriValue);

                            if (symbolsDb != null && symbolsDb.Any(symbolDb => symbolDb.Iri == symbol.Iri))
                            {
                                continue;
                            }
                        }
                    }

                    if (key == "http://example.equinor.com/symbol#width")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        decimal.TryParse(attributeValue, out var width);
                        symbol.Width = width;
                    }

                    if (key == "http://example.equinor.com/symbol#height")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        decimal.TryParse(attributeValue, out var height);
                        symbol.Height = height;
                    }
                    if (key == "http://example.equinor.com/symbol#hasSerialization")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        symbol.Path = attributeValue;
                    }

                    if (key == "http://www.w3.org/2000/01/rdf-schema#label")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        symbol.Label = attributeValue;
                    }

                    if (key == "http://purl.org/dc/terms/description")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        symbol.Description = attributeValue;
                    }
                    if (key == "http://example.equinor.com/symbol#positionX")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        decimal.TryParse(attributeValue, out var x);
                        symbol.ConnectionPoints.Add(new ConnectionPointFromCL { X = x, });
                    }
                    if (key == "http://example.equinor.com/symbol#positionY")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        decimal.TryParse(attributeValue, out var y);
                        var currentSymbol = symbol.ConnectionPoints.LastOrDefault();
                        currentSymbol.Y = y;
                    }

                    if (key == "http://purl.org/dc/terms/description")
                    {
                        var attributeValue = ((LiteralNode) queryResultItem[2]).Value;
                        symbol.Description = attributeValue;
                    }

                    if (queryResultItem == items.LastOrDefault())
                    {
                        symbols.Add(symbol);
                    }
                }
            }
        }

        var engineeringSymbols = new List<EngineeringSymbol>();

        foreach (var item in symbols)
        {
            var connectionPoints = new List<ConnectionPoint>();
            foreach (var connectionPoint in item.ConnectionPoints)
            {
                connectionPoints.Add(new ConnectionPoint { Identifier = connectionPoint.Identifier, PositionX = connectionPoint.X, PositionY = connectionPoint.Y });
            }

            engineeringSymbols.Add(new EngineeringSymbol { ConnectionPoints = connectionPoints, Description = item.Description, Height = item.Height, Iri = item.Iri, Width = item.Width, Label = item.Label, Path = item.Path });
        }

        await symbolRepository.Create(engineeringSymbols);
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
