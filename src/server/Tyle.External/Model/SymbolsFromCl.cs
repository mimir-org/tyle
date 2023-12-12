using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyle.External.Model
{
    

    public class TestSymFromCL
    {
        [JsonProperty("@id")]
        public Guid Id { get; set; }

        [JsonProperty("@graph")]
        public List<TestObject> Objects { get; set; }

    }

    public class TestObject
    {

    }

    
    //public class SymPositionY
    //{

    //    [JsonProperty("@value")]
    //    public string Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class SymPositionX
    //{

    //    [JsonProperty("@value")]
    //    public string Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class DcIssued
    //{

    //    [JsonProperty("@value")]
    //    public DateTime Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class SymHasCenterOfRotation
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }
    //}

    //public class DcModified
    //{

    //    [JsonProperty("@value")]
    //    public DateTime Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class DcCreated
    //{

    //    [JsonProperty("@value")]
    //    public DateTime Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class SymHasSerialization
    //{

    //    [JsonProperty("@value")]
    //    public string Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class DcCreator
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }
    //}

    //public class PavPreviousVersion
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }
    //}

    //public class SymHasConnectionPoint
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }

    //    [JsonProperty("sym:connectorDirection")]
    //    public int? SymConnectorDirection { get; set; }

    //    [JsonProperty("sym:positionX")]
    //    public int? SymPositionX { get; set; }

    //    [JsonProperty("sym:positionY")]
    //    public int? SymPositionY { get; set; }

    //    [JsonProperty("dc:identifier")]
    //    public string DcIdentifier { get; set; }
    //}

    //public class SymConnectorDirection
    //{

    //    [JsonProperty("@value")]
    //    public string Value { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }
    //}

    //public class DctermsCreator
    //{

    //    [JsonProperty("foaf:mbox")]
    //    public string FoafMbox { get; set; }

    //    [JsonProperty("foaf:name")]
    //    public string FoafName { get; set; }
    //}

    //public class Graph
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }

    //    [JsonProperty("sym:positionY")]
    //    public SymPositionY SymPositionY { get; set; }

    //    [JsonProperty("sym:positionX")]
    //    public SymPositionX SymPositionX { get; set; }

    //    [JsonProperty("@type")]
    //    public string Type { get; set; }

    //    [JsonProperty("esmde:id")]
    //    public string EsmdeId { get; set; }

    //    [JsonProperty("dc:description")]
    //    public string DcDescription { get; set; }

    //    [JsonProperty("sym:height")]
    //    public object SymHeight { get; set; }

    //    [JsonProperty("dc:identifier")]
    //    public string DcIdentifier { get; set; }

    //    [JsonProperty("esmde:oid")]
    //    public string EsmdeOid { get; set; }

    //    [JsonProperty("rdfs:label")]
    //    public string RdfsLabel { get; set; }

    //    [JsonProperty("dc:issued")]
    //    public DcIssued DcIssued { get; set; }

    //    [JsonProperty("sym:hasShape")]
    //    public object SymHasShape { get; set; }

    //    [JsonProperty("esmde:status")]
    //    public string EsmdeStatus { get; set; }

    //    [JsonProperty("sym:hasCenterOfRotation")]
    //    public SymHasCenterOfRotation SymHasCenterOfRotation { get; set; }

    //    [JsonProperty("pav:version")]
    //    public string PavVersion { get; set; }

    //    [JsonProperty("dc:modified")]
    //    public DcModified DcModified { get; set; }

    //    [JsonProperty("dc:created")]
    //    public DcCreated DcCreated { get; set; }

    //    [JsonProperty("sym:width")]
    //    public object SymWidth { get; set; }

    //    [JsonProperty("sym:hasSerialization")]
    //    public SymHasSerialization SymHasSerialization { get; set; }

    //    [JsonProperty("dc:creator")]
    //    public DcCreator DcCreator { get; set; }

    //    [JsonProperty("pav:previousVersion")]
    //    public PavPreviousVersion PavPreviousVersion { get; set; }

    //    [JsonProperty("sym:hasConnectionPoint")]
    //    public IList<SymHasConnectionPoint> SymHasConnectionPoint { get; set; }

    //    [JsonProperty("foaf:mbox")]
    //    public string FoafMbox { get; set; }

    //    [JsonProperty("foaf:name")]
    //    public string FoafName { get; set; }

    //    [JsonProperty("sym:connectorDirection")]
    //    public SymConnectorDirection SymConnectorDirection { get; set; }

    //    [JsonProperty("sym:drawColor")]
    //    public string SymDrawColor { get; set; }

    //    [JsonProperty("sym:fillColor")]
    //    public string SymFillColor { get; set; }

    //    [JsonProperty("dcterms:creator")]
    //    public DctermsCreator DctermsCreator { get; set; }
    //}

    //public class Example
    //{

    //    [JsonProperty("@id")]
    //    public string Id { get; set; }

    //    [JsonProperty("@graph")]
    //    public IList<Graph> Graph { get; set; }

    //    [JsonProperty("@context")]
    //    public object Context { get; set; }
    //}

}
