using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Attributes
{
    public class ValueConstraint
    {
        public Guid AttributeId { get; set; }
        public AttributeType Attribute { get; set; }
        public ConstraintType ConstraintType { get; set; }
        public XsdDataType DataType { get; set; }
        public int? MinCount { get; set; }
        public int? MaxCount { get; set; }
        public string? Value { get; set; }
        public ICollection<ValueListEntry>? ValueList { get; set; }
        public string? Pattern { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public bool? MinInclusive { get; set; }
        public bool? MaxInclusive { get; set; }
    }
}