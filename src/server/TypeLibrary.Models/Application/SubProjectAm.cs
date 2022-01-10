using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TypeLibrary.Models.Application
{
    public class SubProjectAm : IValidatableObject
    {
        [Required]
        public string FromProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<string> Nodes { get; set; }
        public ICollection<string> Edges { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nodes == null || !Nodes.Any())
            {
                yield return new ValidationResult("Number of nodes must be greater than 0", new List<string> {"Nodes"});
            }

            if (Nodes != null && Nodes.Any())
            {
                if(Nodes.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList().Any())
                    yield return new ValidationResult("Duplicate node id's detected", new List<string> { "Nodes" });

                if (Nodes.Any(string.IsNullOrWhiteSpace))
                    yield return new ValidationResult("Empty node id's detected", new List<string> { "Nodes" });
            }

            if(Edges != null && Edges.Any())
            {
                if(Edges.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList().Any())
                    yield return new ValidationResult("Duplicate edge id's detected", new List<string> { "Edges" });

                if (Edges.Any(string.IsNullOrWhiteSpace))
                    yield return new ValidationResult("Empty node id's detected", new List<string> { "Edges" });
            }
        }
    }
}
