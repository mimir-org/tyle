namespace TypeLibrary.Models.Application
{
    public class CombinedAttribute
    {
        public string QualifierId { get; set; }
        public string Qualifier { get; set; }

        public string SourceId { get; set; }
        public string Source { get; set; }

        public string ConditionId { get; set; }
        public string Condition { get; set; }

        public string Combined => $"({Qualifier ?? ""}), ({Source ?? ""}), ({Condition ?? ""})";
    }
}
