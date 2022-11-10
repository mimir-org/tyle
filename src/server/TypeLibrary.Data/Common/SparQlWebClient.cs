using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Models;
using VDS.RDF.Query;

namespace TypeLibrary.Data.Common
{
    public class SparQlWebClient
    {
        #region Constants

        public const string PcaEndPoint = @"https://rds.posccaesar.org/ontology/fuseki/ontology/sparql";

        public const string PcaUnitAllQuery = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl: <http://www.w3.org/2002/07/owl#>
            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
            PREFIX om: <http://www.ontology-of-units-of-measure.org/resource/om-2/>
            select ?uom ?uom_label ?default_uom_symbol {
	            ?uom a lis:Scale ; rdfs:label ?uom_label ; om:symbol ?default_uom_symbol
            }
            order by ?uom_label";

        public const string PcaAttributeAllQuery = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:  <http://www.w3.org/2002/07/owl#>
            PREFIX lis:  <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:  <http://rds.posccaesar.org/ontology/plm/rdl/>
            PREFIX om:   <http://www.ontology-of-units-of-measure.org/resource/om-2/>
            select ?quantity ?quantity_label ?datum_type ?datum_type_label ?default_uom ?default_uom_label ?uom ?uom_label {
                OPTIONAL { 
		            ?quantity rdl:PCA_100000510 ?default_uom .
		            ?default_uom a lis:Scale ; rdfs:label ?default_uom_label 
	            }
                ?quantity rdfs:label ?quantity_label ;
                rdfs:subClassOf [ a owl:Restriction ; owl:allValuesFrom  ?datum_type ; owl:onProperty lis:qualityQuantifiedAs ] .
                ?datum_type rdfs:label ?datum_type_label .
                ?uom rdfs:label ?uom_label ; a [ a owl:Restriction ; owl:allValuesFrom ?datum_type ; owl:onProperty [ owl:inverseOf  lis:datumUOM ] ] .
            }
            order by ?quantity_label";

        //TODO
        public const string PcaPurposeAllQuery = @"";

        public const string QuantityDatumRangeSpecifying = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl: <http://www.w3.org/2002/07/owl#>
            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
            select ?datum ?datum_label {
              ?datum rdfs:subClassOf rdl:PCA_100004035 ; rdfs:label ?datum_label
            }
            order by ?datum_label";

        public const string QuantityDatumSpecifiedScope = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl: <http://www.w3.org/2002/07/owl#>
            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
            select ?datum ?datum_label {
              ?datum rdfs:subClassOf rdl:PCA_100004034 ; rdfs:label ?datum_label
            }
            order by ?datum_label";

        public const string QuantityDatumSpecifiedProvenance = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl: <http://www.w3.org/2002/07/owl#>
            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
            select ?datum ?datum_label {
              ?datum rdfs:subClassOf rdl:PCA_100004033 ; rdfs:label ?datum_label
            }
            order by ?datum_label";

        public const string QuantityDatumRegularitySpecified = @"PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl: <http://www.w3.org/2002/07/owl#>
            PREFIX lis: <http://rds.posccaesar.org/ontology/lis14/rdl/>
            PREFIX rdl:   <http://rds.posccaesar.org/ontology/plm/rdl/>
            select ?datum ?datum_label {
              ?datum rdfs:subClassOf rdl:PCA_100004036 ; rdfs:label ?datum_label
            }
            order by ?datum_label";

        #endregion

        public string EndPoint { get; set; }
        public string Query { get; set; }

        public IEnumerable<T> Get<T>() where T : class, new()
        {
            if (string.IsNullOrEmpty(EndPoint) || string.IsNullOrEmpty(Query))
                yield break;

            var endpoint = new SparqlRemoteEndpoint(new Uri(EndPoint));
            var results = endpoint.QueryWithResultSet(Query);

            if (results == null || !results.Any())
                yield break;

            var propertyFinder = new GenericPropertyFinder<T>();
            var model = new T();
            var props = propertyFinder.Get(model).ToList();

            foreach (var result in results)
            {
                var obj = new T();

                foreach (var prop in props)
                {
                    if (result.TryGetValue(prop.ToLower(), out var node))
                    {
                        obj.GetType().GetProperty(prop)?.SetValue(obj, node?.ToString());
                    }
                }

                yield return obj;
            }
        }
    }
}