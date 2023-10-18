namespace Tyle.Converters;

public class JsonLdConstants
{
    public const string Context = """
                                  {
                                    "@context": [
                                        "http://jsonld-context.dyreriket.xyz/rdfs.json",
                                        "http://jsonld-context.dyreriket.xyz/sh.json",
                                        "http://jsonld-context.dyreriket.xyz/pav.json",
                                        "http://jsonld-context.dyreriket.xyz/dc.json",
                                        "http://jsonld-context.dyreriket.xyz/skos.json",
                                        "https://imf-lab.gitlab.io/imf-ontology/out/json/imf-context.json",
                                        {
                                            "@version": 1.1,
                                            "dc": "http://purl.org/dc/elements/1.1/",
                                            "ex": "http://example.com/example/",
                                            "foaf": "http://xmlns.com/foaf/0.1/",
                                            "imf": "http://ns.imfid.org/imf#",
                                            "owl": "http://www.w3.org/2002/07/owl#",
                                            "pav": "http://purl.org/pav/",
                                            "pca-plm": "http://rds.posccaesar.org/ontology/plm/rdl/",
                                            "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                            "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                            "sh": "http://www.w3.org/ns/shacl#",
                                            "skos": "http://www.w3.org/2004/02/skos/core#",
                                            "vann": "http://purl.org/vocab/vann/",
                                            "vs": "http://www.w3.org/2003/06/sw-vocab-status/ns#",
                                            "xsd": "http://www.w3.org/2001/XMLSchema#"
                                        }
                                    ]
                                  }

                                  """;

    public const string Frame = """
                                {
                                    "@context": [
                                        "http://jsonld-context.dyreriket.xyz/rdfs.json",
                                        "http://jsonld-context.dyreriket.xyz/sh.json",
                                        "http://jsonld-context.dyreriket.xyz/pav.json",
                                        "http://jsonld-context.dyreriket.xyz/dc.json",
                                        "http://jsonld-context.dyreriket.xyz/skos.json",
                                        "https://imf-lab.gitlab.io/imf-ontology/out/json/imf-context.json",
                                        {
                                            "@version": 1.1,
                                            "dc": "http://purl.org/dc/elements/1.1/",
                                            "ex": "http://example.com/example/",
                                            "foaf": "http://xmlns.com/foaf/0.1/",
                                            "imf": "http://ns.imfid.org/imf#",
                                            "owl": "http://www.w3.org/2002/07/owl#",
                                            "pav": "http://purl.org/pav/",
                                            "pca-plm": "http://rds.posccaesar.org/ontology/plm/rdl/",
                                            "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                            "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                            "sh": "http://www.w3.org/ns/shacl#",
                                            "skos": "http://www.w3.org/2004/02/skos/core#",
                                            "vann": "http://purl.org/vocab/vann/",
                                            "vs": "http://www.w3.org/2003/06/sw-vocab-status/ns#",
                                            "xsd": "http://www.w3.org/2001/XMLSchema#"
                                        }
                                    ],
                                    "sh:property": {}
                                }
                                """;
}
