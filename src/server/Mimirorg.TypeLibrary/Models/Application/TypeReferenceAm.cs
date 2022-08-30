using System.ComponentModel.DataAnnotations;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TypeReferenceAm : IEquatable<TypeReferenceAm>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Iri { get; set; }

        [Required]
        public string Source { get; set; }


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