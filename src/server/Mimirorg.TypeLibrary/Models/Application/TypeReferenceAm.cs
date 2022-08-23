using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TypeReferenceAm : IEqualityComparer<TypeReferenceAm>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Iri { get; set; }

        public bool Equals(TypeReferenceAm x, TypeReferenceAm y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name && x.Iri == y.Iri;
        }

        public int GetHashCode(TypeReferenceAm obj)
        {
            return HashCode.Combine(obj.Name, obj.Iri);
        }
    }
}
