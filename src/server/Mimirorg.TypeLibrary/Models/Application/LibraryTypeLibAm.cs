using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class LibraryTypeLibAm : IValidatableObject
    {
        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Domain { get; set; }

        public string Description { get; set; }

        [EnumDataType(typeof(Aspect))]
        public Aspect Aspect { get; set; }

        [EnumDataType(typeof(ObjectType))]
        public ObjectType ObjectType { get; set; }

        public string Iri { get; set; }

        [Required]
        public string RdsId { get; set; }

        [Required] 
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";

        [Required]
        public string PurposeId { get; set; }

        public ICollection<TerminalLibCm> Terminals { get; set; }
        public string BlobId { get; set; }
        public ICollection<string> AttributeIdList { get; set; }
        public string AttributeAspectId { get; set; }
        public ICollection<AttributePredefinedLibCm> AttributesPredefined { get; set; }
        public string TerminalId { get; set; }
        public ICollection<string> Simple { get; set; }
        public ICollection<CollectionLibAm> Collections { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}-{RdsId}-{Aspect}-{Version}";

        public string Version { get; set; }
        public string FirstVersionId { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((ObjectType == ObjectType.Interface || ObjectType == ObjectType.Transport) && string.IsNullOrEmpty(TerminalId))
            {
                yield return new ValidationResult(

                    "If object typeDm is of typeDm interface or transport, TerminalId must be set.",
                    new List<string> { "TerminalId" }
                );
            }
        }
    }
}
