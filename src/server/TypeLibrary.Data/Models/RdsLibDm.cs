using Mimirorg.TypeLibrary.Enums;
using System;

namespace TypeLibrary.Data.Models
{
    public class RdsLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

    }
}