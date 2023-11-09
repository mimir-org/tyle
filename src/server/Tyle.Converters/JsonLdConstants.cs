namespace Tyle.Converters;

public class JsonLdConstants
{
    public const string Context = """
                                  {
                                    "@context": {
                                        "@version": 1.1,
                                        "dcterms": "http://purl.org/dc/terms/",
                                        "foaf": "http://xmlns.com/foaf/0.1/",
                                        "imf": "http://ns.imfid.org/imf#",
                                        "pav": "http://purl.org/pav/",
                                        "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                        "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                        "sh": "http://www.w3.org/ns/shacl#",
                                        "skos": "http://www.w3.org/2004/02/skos/core#",
                                        "xsd": "http://www.w3.org/2001/XMLSchema#",
                                          
                                        "rdfs:subClassOf": {
                                            "@type": "@id"
                                        },
                                         
                                        "sh:datatype": {
                                            "@type": "@id"
                                        },
                                        "sh:in": {
                                            "@container": "@list"
                                        },
                                        "sh:maxCount": {
                                            "@type": "xsd:integer"
                                        },
                                        "sh:minCount": {
                                           "@type": "xsd:integer"
                                        },
                                        "sh:node": {
                                            "@type": "@id"
                                        },
                                        "sh:path": {
                                            "@type": "@id"
                                        },
                                        "sh:pattern": {
                                            "@type": "xsd:string"
                                        },
                                        
                                        "pav:version": {
                                            "@type": "xsd:string"
                                        },
                                        
                                        "imf:classifier": {
                                            "@type": "@id"
                                        },
                                        "imf:hasAspect": {
                                            "@type": "@id"
                                        },
                                        "imf:hasAttribute": {
                                            "@type": "@id"
                                        },
                                        "imf:hasAttributeQualifier": {
                                            "@type": "@id"
                                        },
                                        "imf:hasInputTerminal": {
                                            "@type": "@id"
                                        },
                                        "imf:hasOutputTerminal": {
                                            "@type": "@id"
                                        },
                                        "imf:hasTerminal": {
                                            "@type": "@id"
                                        },
                                        "imf:hasTerminalQualifier": {
                                            "@type": "@id"
                                        },
                                        "imf:medium": {
                                            "@type": "@id"
                                        },
                                        "imf:predicate": {
                                            "@type": "@id"
                                        },
                                        "imf:purpose": {
                                            "@type": "@id"
                                        },
                                        "imf:uom": {
                                            "@type": "@id"
                                        }
                                    }
                                  }
                                  """;

    public const string Frame = """
                                {
                                    "@context": {
                                    "@version": 1.1,
                                "dcterms": "http://purl.org/dc/terms/",
                                    "foaf": "http://xmlns.com/foaf/0.1/",
                                    "imf": "http://ns.imfid.org/imf#",
                                    "pav": "http://purl.org/pav/",
                                    "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                    "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                    "sh": "http://www.w3.org/ns/shacl#",
                                    "skos": "http://www.w3.org/2004/02/skos/core#",
                                    "xsd": "http://www.w3.org/2001/XMLSchema#",
                                      
                                    "rdfs:subClassOf": {
                                        "@type": "@id"
                                    },
                                     
                                    "sh:datatype": {
                                        "@type": "@id"
                                    },
                                    "sh:in": {
                                        "@container": "@list"
                                    },
                                    "sh:maxCount": {
                                        "@type": "xsd:integer"
                                    },
                                    "sh:minCount": {
                                       "@type": "xsd:integer"
                                    },
                                    "sh:node": {
                                        "@type": "@id"
                                    },
                                    "sh:path": {
                                        "@type": "@id"
                                    },
                                    
                                    "imf:classifier": {
                                        "@type": "@id"
                                    },
                                    "imf:hasAspect": {
                                        "@type": "@id"
                                    },
                                    "imf:hasAttribute": {
                                        "@type": "@id"
                                    },
                                    "imf:hasAttributeQualifier": {
                                        "@type": "@id"
                                    },
                                    "imf:hasInputTerminal": {
                                        "@type": "@id"
                                    },
                                    "imf:hasOutputTerminal": {
                                        "@type": "@id"
                                    },
                                    "imf:hasTerminal": {
                                        "@type": "@id"
                                    },
                                    "imf:hasTerminalQualifier": {
                                        "@type": "@id"
                                    },
                                    "imf:medium": {
                                        "@type": "@id"
                                    },
                                    "imf:predicate": {
                                        "@type": "@id"
                                    },
                                    "imf:purpose": {
                                        "@type": "@id"
                                    },
                                    "imf:uom": {
                                        "@type": "@id"
                                    }
                                },
                                    "@type": "sh:NodeShape",
                                    "sh:property": {
                                        "sh:path": {},
                                        "sh:node": {
                                            "@omitDefault": true,
                                            "@embed": "@never"
                                        }
                                    }
                                }
                                """;
}