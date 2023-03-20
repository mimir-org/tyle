using System;

namespace TypeLibrary.Data.Models
{
    public class UnitLibDm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string Symbol { get; set; }
        public string State { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}