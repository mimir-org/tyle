using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeLibCm
    {
        public string Id { get; set; }
        public string ParentName { get; set; }
        public string ParentIri { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }
        public string AttributeQualifier { get; set; }
        public string AttributeSource { get; set; }
        public string AttributeCondition { get; set; }
        public string AttributeFormat { get; set; }
        public Aspect Aspect { get; set; }
        public Discipline Discipline { get; set; }
        public HashSet<string> Tags { get; set; }
        public Select Select { get; set; }
        public ICollection<string> SelectValues { get; set; }
        public ICollection<UnitLibCm> Units { get; set; }
        public string Description => CreateDescription();
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Kind => nameof(AttributeLibCm);
        public AttributeType AttributeType { get; set; }

        private string CreateDescription()
        {
            var text = string.Empty;

            if (!string.IsNullOrWhiteSpace(AttributeSource) && AttributeSource != "NotSet")
                text += AttributeSource + " ";

            text += Name;

            var subText = string.Empty;

            if (!string.IsNullOrWhiteSpace(AttributeQualifier) && AttributeQualifier != "NotSet")
                subText = AttributeQualifier;

            if (!string.IsNullOrWhiteSpace(AttributeCondition) && AttributeCondition != "NotSet")
                subText += ", " + AttributeCondition;

            if (!string.IsNullOrEmpty(subText))
                text += " - " + subText;

            return text;
        }
    }
}