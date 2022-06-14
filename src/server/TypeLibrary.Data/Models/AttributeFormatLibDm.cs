using System;

namespace TypeLibrary.Data.Models
{
    public class AttributeFormatLibDm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}