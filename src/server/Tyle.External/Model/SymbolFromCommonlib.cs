using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyle.External.Model
{
    public class SymbolFromCommonlibRoot
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@graph")]
        public List<GraphTypes> Symbol { get; set; }
    }
    
    public class GraphTypes
    {
        //[JsonProperty("@graph")]
        //public SymbolFromCommonlib SymbolFromCommonlib { get; set; }
        [JsonProperty("@id")]
        public string Id { get; set; }
        //[JsonProperty("rdfs:label")]
        //public string Label { get; set; }
        //[JsonProperty("dc:identifier")]
        //public string Identifier { get; set; }
        ////[JsonProperty("@iri")]
        ////public string Iri { get; set; }
        //[JsonProperty("@description")]
        //public string Description { get; set; }
        //[JsonProperty("sym:hasSerialization:value")]
        //public string Path { get; set; }
        //[JsonProperty("sym:height")]
        //public SymbolDimensions Height { get; set; }
        [JsonProperty("sym:width")]
        public SymbolDimensions Width { get; set; }
        ////public List<Connector> Connectors { get; set; }
        //[JsonProperty("@value")]
        //public string Value { get; set; }
        //[JsonProperty("@type")]
        //public string Type { get; set; }

    }
    

    public class SymbolFromCommonlib
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("rdfs:label")]
        public string Label { get; set; }
        [JsonProperty("dc:identifier")]
        public string Identifier { get; set; }
        //[JsonProperty("@iri")]
        //public string Iri { get; set; }
        [JsonProperty("@description")]
        public string Description { get; set; }
        [JsonProperty("sym:hasSerialization:value")]
        public string Path { get; set; }
        [JsonProperty("sym:height")]
        public SymbolDimensions Height { get; set; }
        [JsonProperty("sym:width")]
        public SymbolDimensions Width { get; set; }
        //public List<Connector> Connectors { get; set; }
        [JsonProperty("@value")]
        public int Value { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }

    }

    public class SymbolDimensions
    {
        //[JsonProperty("@value")]
        //public int Value { get; set; }
        //[JsonProperty("@type")]
        //public string Type { get; set; }

    }


    public class Connector
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int Direction { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

    }
}
