using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
// ReSharper disable InconsistentNaming

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }
        public string QuantityDatumSpecifiedScope { get; set; }
        public string QuantityDatumSpecifiedProvenance { get; set; }
        public string QuantityDatumRangeSpecifying { get; set; }
        public string QuantityDatumRegularitySpecified { get; set; }
        public Aspect Aspect { get; set; }
        public Discipline Discipline { get; set; }
        public Select Select { get; set; }

        public ICollection<string> SelectValues { get; set; }
        public ICollection<UnitLibCm> Units { get; set; }
        public string Description => CreateDescription();
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public string Kind => nameof(AttributeLibCm);

        private string CreateDescription()
        {
            var text = $"{Name} {QuantityDatumSpecifiedScope} {QuantityDatumSpecifiedProvenance} {QuantityDatumRangeSpecifying} {QuantityDatumRegularitySpecified}";
            return text.RemoveWhitespaces();
        }
    }
}