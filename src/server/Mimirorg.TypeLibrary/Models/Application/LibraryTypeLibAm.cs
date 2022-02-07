using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class LibraryTypeLibAm : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        public string Domain { get; set; }

        public string Description { get; set; }

        [EnumDataType(typeof(Aspect))]
        public Aspect Aspect { get; set; }

        [EnumDataType(typeof(ObjectType))]
        public ObjectType ObjectType { get; set; }

        public string SemanticReference { get; set; }

        [Required]
        public string RdsId { get; set; }

        [Required] 
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";

        [Required]
        public string Purpose { get; set; }

        public ICollection<TerminalLibCm> TerminalTypes { get; set; }
        public string SymbolId { get; set; }
        public ICollection<string> AttributeStringList { get; set; }
        public string LocationType { get; set; }
        public ICollection<AttributePredefinedLibCm> PredefinedAttributes { get; set; }
        public string TerminalTypeId { get; set; }
        public ICollection<string> SimpleTypes { get; set; }
        public ICollection<CollectionLibAm> Categories { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}-{RdsId}-{Aspect}-{Version}";

        public string Version { get; set; }
        public string TypeId { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((ObjectType == ObjectType.Interface || ObjectType == ObjectType.Transport) && string.IsNullOrEmpty(TerminalTypeId))
            {
                yield return new ValidationResult(

                    "If object typeDm is of typeDm interface or transport, TerminalId must be set.",
                    new List<string> { "TerminalId" }
                );
            }
        }
    }
}
