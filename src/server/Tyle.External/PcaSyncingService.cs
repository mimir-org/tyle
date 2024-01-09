using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Common;
using Tyle.Core.Common;
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
        await SyncClassifiers();
        await SyncPredicates();
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
                            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
                            PREFIX pca: <http://data.posccaesar.org/rdl/>
                            PREFIX skos: <http://www.w3.org/2004/02/skos/core#>
                            select * {
                              ?process rdfs:subClassOf+ <http://rds.posccaesar.org/ontology/plm/rdl/PCA_100000001> ;
                                  rdfs:label ?label ;
                                  rdfs:subClassOf ?superclass .
                              OPTIONAL {
                               ?process skos:exactMatch | skos:closeMatch | skos:relatedMatch ?rdl .
                               
                               SERVICE <https://data.posccaesar.org/rdl/sparql> { 
                                  ?rdl  <http://data.posccaesar.org/rdl/hasDefinition> ?def 
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

    private async Task SyncClassifiers()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();


        var classifierRepository = scope.ServiceProvider.GetService<IClassifierRepository>();

        if (classifierRepository == null)
        {
            return;
        }

        var classifiersQuery = """
                            PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                            PREFIX owl: <http://www.w3.org/2002/07/owl#>
                            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
                            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
                            PREFIX pca: <http://data.posccaesar.org/rdl/>
                            PREFIX skos: <http://www.w3.org/2004/02/skos/core#>
                            select * {
                              ?process rdfs:subClassOf+ <http://rds.posccaesar.org/ontology/plm/rdl/PCA_100001001> ;
                                  rdfs:label ?label ;
                                  rdfs:subClassOf ?superclass .
                              OPTIONAL {
                               ?process skos:exactMatch | skos:closeMatch | skos:relatedMatch ?rdl .
                               
                               SERVICE <https://data.posccaesar.org/rdl/sparql> { 
                                  ?rdl  <http://data.posccaesar.org/rdl/hasDefinition> ?def 
                                } 
                              }
                            }
                            order by ?label
                            """;

        var classifiers = await _queryClient.QueryWithResultSetAsync(classifiersQuery);

        var classifiersDb = (await classifierRepository.GetAll()).ToList();

        var classifiersToCreate = new List<RdlClassifierRequest>();

        foreach (var classifier in classifiers)
        {
            var iriString = classifier["process"].ToString();

            if (iriString == null || !Uri.TryCreate(iriString, UriKind.Absolute, out var iri) || classifiersDb.Any(savedClassifier => savedClassifier.Iri.Equals(iri)))
            {
                continue;
            }

            string? nameString = null;

            if (classifier["label"] != null)
            {
                nameString = classifier["label"].ToString().Split('^')[0];
            }

            if (nameString == null) continue;

            string? descriptionString = null;

            if (classifier["def"] != null)
            {
                descriptionString = classifier["def"].ToString().Split('^')[0];
            }

            var classifierRequest = new RdlClassifierRequest
            {
                Name = nameString,
                Description = descriptionString == null || descriptionString.Length > 0 ? descriptionString : null,
                Iri = iriString
            };

            if (classifiersToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(classifierRequest.Iri)))) continue;

            classifiersToCreate.Add(classifierRequest);
        }

        await classifierRepository.Create(classifiersToCreate, ReferenceSource.Pca);
    }

    private async Task SyncPredicates()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();


        var predicatesRepository = scope.ServiceProvider.GetService<IPredicateRepository>();

        if (predicatesRepository == null)
        {
            return;
        }

        var predicatesQuery = """
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

        var predicates = await _queryClient.QueryWithResultSetAsync(predicatesQuery);

        var predicatesDb = (await predicatesRepository.GetAll()).ToList();

        var predicatesToCreate = new List<RdlPredicateRequest>();

        foreach (var predicate in predicates)
        {
            var iriString = predicate["process"].ToString();

            if (iriString == null || !Uri.TryCreate(iriString, UriKind.Absolute, out var iri) || predicatesDb.Any(savedPredicate => savedPredicate.Iri.Equals(iri)))
            {
                continue;
            }

            string? nameString = null;

            if (predicate["label"] != null)
            {
                nameString = predicate["label"].ToString().Split('^')[0];
            }

            if (nameString == null) continue;

            string? descriptionString = null;

            if (predicate["def"] != null)
            {
                descriptionString = predicate["def"].ToString().Split('^')[0];
            }

            var predicateRequest = new RdlPredicateRequest
            {
                Name = nameString,
                Description = descriptionString == null || descriptionString.Length > 0 ? descriptionString : null,
                Iri = iriString
            };

            if (predicatesToCreate.Any(x => new Uri(x.Iri).Equals(new Uri(predicateRequest.Iri)))) continue;

            predicatesToCreate.Add(predicateRequest);
        }

        await predicatesRepository.Create(predicatesToCreate, ReferenceSource.Pca);
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