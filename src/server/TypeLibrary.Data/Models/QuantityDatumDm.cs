using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class QuantityDatumDm
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Iri { get; set; }
        public string Description { get; set; }
        public QuantityDatumType QuantityDatumType { get; set; }
    }
}