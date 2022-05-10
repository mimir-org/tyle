﻿namespace TypeLibrary.Data.Models
{
    public class SymbolLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string Data { get; set; }
        public bool Deleted { get; set; }
    }
}