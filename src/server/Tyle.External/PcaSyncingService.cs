using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Common;
using Tyle.Core.Common;
using Tyle.External.Model;
using VDS.RDF.Query;

namespace Tyle.External;

public class PcaSyncingService : IHostedService, IDisposable
{
    private bool _disposedValue;
    private readonly SparqlQueryClient _queryClient;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer;

    private readonly Uri _pcaEndPoint = new("https://rds.posccaesar.org/ontology/fuseki/ontology/sparql");

    public PcaSyncingService(IServiceProvider serviceProvider)
    {
        _queryClient = new SparqlQueryClient(new HttpClient(), _pcaEndPoint);
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
        await SyncUnits();
    }

    private async Task SyncPurposes()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();


        var purposeRepository = scope.ServiceProvider.GetService<IPurposeRepository>();

        if (purposeRepository == null)
        {
            return;
        }

        var purposesQuery = """
                            PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                            PREFIX owl: <http://www.w3.org/2002/07/owl#>
                            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
                            PREFIX rdl: <http://rds.posccaesar.org/ontology/plm/rdl/>
                            PREFIX pca: <http://data.posccaesar.org/rdl/>
                            PREFIX skos: <http://www.w3.org/2004/02/skos/core#>
                            
                            select * {
                              ?process rdfs:subClassOf+ <http://rds.posccaesar.org/ontology/plm/rdl/PCA_100001001> ;
                              rdfs:label ?label ;
                              rdfs:subClassOf ?superclass .
                                                            
                              OPTIONAL {
                                ?process skos:exactMatch | skos:closeMatch | skos:relatedMatch ?rdl .
                             
                                SERVICE <https://data.posccaesar.org/rdl/sparql> {
                                  ?rdl <http://data.posccaesar.org/rdl/hasDefinition> ?def
                                }
                              }
                            }
                            
                            order by ?label
                            """;

        var purposes = await _queryClient.QueryWithResultSetAsync(purposesQuery);

        var purposesDb = (await purposeRepository.GetAll()).ToList();

        var purposesToCreate = new List<RdlPurposeRequest>();

        foreach (var purpose in purposes)
        {
            var iriString = purpose["process"].ToString();

            if (iriString == null || !Uri.TryCreate(iriString, UriKind.Absolute, out var iri) || purposesDb.Any(savedPurpose => savedPurpose.Iri.Equals(iri)))
            {
                continue;
            }

            string? nameString = null;

            if (purpose["label"] != null)
            {
                nameString = purpose["label"].ToString().Split('^')[0];
            }

            if (nameString == null) continue;

            string? descriptionString = null;

            if (purpose["def"] != null)
            {
                descriptionString = purpose["def"].ToString().Split('^')[0];
            }

            var purposeRequest = new RdlPurposeRequest
            {
                Name = nameString,
                Description = descriptionString == null || descriptionString.Length > 0 ? descriptionString : null,
                Iri = iriString
            };

            if (purposesToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(purposeRequest.Iri)))) continue;

            purposesToCreate.Add(purposeRequest);
        }

        await purposeRepository.Create(purposesToCreate, ReferenceSource.Pca);
    }

    private async Task SyncUnits()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();


        var unitRepository = scope.ServiceProvider.GetService<IUnitRepository>();

        if (unitRepository == null)
        {
            return;
        }

        var unitsQuery = """
                            PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                            PREFIX owl: <http://www.w3.org/2002/07/owl#>
                            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
                            PREFIX rdl: <http://rds.posccaesar.org/ontology/plm/rdl/>
                            PREFIX pca: <http://data.posccaesar.org/rdl/>
                            PREFIX skos: <http://www.w3.org/2004/02/skos/core#>
                            
                            select * {
                              ?process rdfs:subClassOf+ <http://rds.posccaesar.org/ontology/lis14/rdl/PhysicalQuantity> ;
                              rdfs:label ?label ;
                              rdfs:subClassOf ?superclass .
                             
                              OPTIONAL {
                                ?process skos:exactMatch | skos:closeMatch | skos:relatedMatch ?rdl .
                             
                                SERVICE <https://data.posccaesar.org/rdl/sparql> { 
                                  ?rdl <http://data.posccaesar.org/rdl/hasDefinition> ?def 
                                } 
                              }
                              FILTER(isIRI(?superclass))
                            }
                            
                            order by ?label
                            """;

        var units = await _queryClient.QueryWithResultSetAsync(unitsQuery);

        var unitsDb = (await unitRepository.GetAll()).ToList();

        var unitsToCreate = new List<RdlUnitRequest>();

        foreach (var unit in units)
        {
            var iriString = unit["process"].ToString();

            if (iriString == null || !Uri.TryCreate(iriString, UriKind.Absolute, out var iri) || unitsDb.Any(savedUnit => savedUnit.Iri.Equals(iri)))
            {
                continue;
            }

            string? nameString = null;

            if (unit["label"] != null)
            {
                nameString = unit["label"].ToString().Split('^')[0];
            }

            if (nameString == null) continue;

            string? descriptionString = null;

            if (unit["def"] != null)
            {
                descriptionString = unit["def"].ToString().Split('^')[0];
            }

            var unitRequest = new RdlUnitRequest
            {
                Name = nameString,
                Description = descriptionString == null || descriptionString.Length > 0 ? descriptionString : null,
                Iri = iriString
            };

            if (unitsToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(unitRequest.Iri)))) continue;

            unitsToCreate.Add(unitRequest);
        }

        await unitRepository.Create(unitsToCreate, ReferenceSource.Pca);
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