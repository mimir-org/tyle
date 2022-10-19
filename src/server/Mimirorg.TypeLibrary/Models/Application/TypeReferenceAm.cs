using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Models.Client;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TypeReferenceAm : IEquatable<TypeReferenceAm>
    {
        public string Id => Iri?.Substring(Iri.LastIndexOf('/') + 1);

        [Required]
        public string Name { get; set; }

        [Required]
        public string Iri { get; set; }

        [Required]
        public string Source { get; set; }

        public ICollection<TypeReferenceSub> Units { get; set; }

        public bool Equals(TypeReferenceAm other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Iri == other.Iri && Source == other.Source;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((TypeReferenceAm) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Iri, Source);
        }
    }
}