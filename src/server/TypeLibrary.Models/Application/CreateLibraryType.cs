using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class CreateLibraryType : IValidatableObject
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

        // NodeType (Object Block)
        public ICollection<TerminalTypeItem> TerminalTypes { get; set; }
        public string SymbolId { get; set; }

        // NodeType (Object Block), TransportType
        public ICollection<string> AttributeTypes { get; set; }

        // Location aspect
        public string LocationType { get; set; }
        public ICollection<PredefinedAttributeAm> PredefinedAttributes { get; set; }

        // InterfaceType, TransportType
        public string TerminalTypeId { get; set; }

        // SimpleType
        public ICollection<string> SimpleTypes { get; set; }

        // Collection
        public ICollection<CollectionAm> Collections { get; set; }

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

                    "If object type is of type interface or transport, TerminalTypeId must be set.",
                    new List<string> { "TerminalTypeId" }
                );
            }
        }
    }
}
